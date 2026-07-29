$(function () {
    // Send antiforgery token on all jQuery AJAX mutating requests (forms + JSON-style payloads).
    var antiforgeryToken = $('meta[name="request-verification-token"]').attr('content')
        || $('input[name="__RequestVerificationToken"]').first().val();
    if (antiforgeryToken) {
        $.ajaxSetup({
            beforeSend: function (xhr, settings) {
                var method = (settings.type || settings.method || 'GET').toUpperCase();
                if (method === 'GET' || method === 'HEAD' || method === 'OPTIONS' || method === 'TRACE') {
                    return;
                }
                xhr.setRequestHeader('RequestVerificationToken', antiforgeryToken);
            }
        });
    }

    init();
    bindPortalListCrudButtons();
});

var portalListRefreshSeq = 0;

function bindPortalListCrudButtons() {
    if (document.documentElement.dataset.portalListCrudBound === '1') {
        return;
    }
    document.documentElement.dataset.portalListCrudBound = '1';

    $(document).on('click', '.opButtonDetail', function () {
        var selectedKey = this.id.replace('DetailItem_', '');
        $('#SelectedKey').val(selectedKey);
        $('#formList').data('SelectedKey', selectedKey);
        openModalPopup($(this).data('module-name'), 'DetailItem', this);
    });

    $(document).on('click', '.opButtonInsert', function (e) {
        // Query Run buttons must not open the Insert modal (they only submit #formList via AJAX).
        if (this.classList && this.classList.contains('portal-query-run')) {
            return;
        }
        if (this.closest && this.closest('form[data-portal-query="true"]')) {
            return;
        }
        openModalPopup($(this).data('module-name'), 'InsertItem', this);
    });

    $(document).on('click', '.opButtonDelete', function () {
        var selectedKey = this.id.replace('DeleteItem_', '');
        $('#SelectedKey').val(selectedKey);
        $('#formList').data('SelectedKey', selectedKey);
        openModalPopup($(this).data('module-name'), 'DeleteItem', this);
    });

    $(document).on('click', '.opButtonUpdate', function () {
        var selectedKey = this.id.replace('UpdateItem_', '');
        $('#SelectedKey').val(selectedKey);
        $('#formList').data('SelectedKey', selectedKey);
        openModalPopup($(this).data('module-name'), 'UpdateItem', this);
    });
}

function openModalPopup(moduleName, actionName, triggerBtn) {
    var popupUrl = '/' + moduleName + '/' + actionName;

    $.ajax({
        type: 'POST',
        url: popupUrl,
        processData: false,
        data: $('#formList').serialize(),
        beforeSend: function () {
            showPortalCrudBusy(triggerBtn);
        },
        success: function (data) {
            // Cookie auth may follow a 302 and return the login page as HTML with 200.
            if (isPortalLoginHtml(data)) {
                window.location = '/Login/Index';
                return;
            }

            $('#itemDetailsContainer').html(data);
            $('#itemDetailsContainer .modal-content').addClass('portal-entity-modal');
            enhancePortalEntityForm(document.getElementById('itemDetailsContainer'));
            var modalElement = document.getElementById('itemDetailsModal');
            var detailsRoot = document.getElementById('itemDetailsContainer');

            function initModalEditors() {
                loadJsonAllEditors();
                loadYamlAllEditors(detailsRoot);
                loadUmlAllEditors();
                initDatePickers(detailsRoot);
                initSearchableSelects(detailsRoot);
            }

            function resizeYamlEditors() {
                if (!detailsRoot) {
                    return;
                }
                $(detailsRoot).find('.yamleditor-class').each(function () {
                    if (this.env && this.env.editor) {
                        this.env.editor.resize(true);
                    }
                });
            }

            initModalEditors();

            if (modalElement && typeof bootstrap !== 'undefined') {
                var modal = bootstrap.Modal.getOrCreateInstance(modalElement);
                $(modalElement).one('shown.bs.modal', resizeYamlEditors);
                modal.show();
            } else {
                $('#itemDetailsModal').one('shown.bs.modal', resizeYamlEditors).modal('show');
            }
        },
        error: function (xhr, textStatus, error) {
            handlePortalAjaxError(xhr, {
                reload: true,
                fallbackMessage: error || textStatus || 'Request failed'
            });
        },
        complete: function () {
            hidePortalCrudBusy(triggerBtn);
        }
    });
}

function getListPartialUrl() {
    var form = document.getElementById('formList');
    if (form && form.dataset.listPartialUrl) {
        return form.dataset.listPartialUrl;
    }

    var path = window.location.pathname.replace(/\/?$/, '');
    if (/\/List$/i.test(path)) {
        return path.replace(/\/List$/i, '/ListPartial');
    }

    return path + '/ListPartial';
}

function setPortalListRegionLoading(region, loading) {
    if (!region) {
        return;
    }

    var overlay = region.querySelector(':scope > .portal-list-region__loading');
    if (loading) {
        region.classList.add('is-loading');
        region.setAttribute('aria-busy', 'true');
        if (!overlay) {
            overlay = document.createElement('div');
            overlay.className = 'portal-list-region__loading';
            overlay.setAttribute('role', 'status');
            overlay.setAttribute('aria-live', 'polite');
            overlay.innerHTML =
                '<div class="portal-list-region__loading-panel">' +
                '<span class="portal-list-region__loading-spinner" aria-hidden="true"></span>' +
                '<span class="portal-list-region__loading-label">Loading…</span>' +
                '</div>';
            region.appendChild(overlay);
        }
        return;
    }

    region.classList.remove('is-loading');
    region.removeAttribute('aria-busy');
    if (overlay) {
        overlay.remove();
    }
}

function hidePortalEntityModal() {
    var modalElement = document.getElementById('itemDetailsModal');
    if (modalElement && typeof bootstrap !== 'undefined') {
        var modal = bootstrap.Modal.getInstance(modalElement);
        if (modal) {
            modal.hide();
        }
    } else if (typeof $ !== 'undefined') {
        $('#itemDetailsModal').modal('hide');
    }
}

/**
 * After Insert/Update/Delete: close modal, toast, refresh list region (no full reload).
 */
function finishPortalCrudSuccess(successMessage, operation) {
    hidePortalEntityModal();

    if (typeof showPortalToast === 'function' && successMessage) {
        showPortalToast(successMessage, 'success');
    }

    // Delete on last row of a page — PrepareModel clamps CurrentPage on refresh.
    var op = (operation || '').toLowerCase();
    if (op === 'insert') {
        var currentPage = document.getElementById('CurrentPage');
        if (currentPage) {
            currentPage.value = '1';
        }
    }

    if (document.getElementById('list-region') && typeof refreshListRegion === 'function') {
        refreshListRegion();
        return;
    }

    if (typeof queuePortalToast === 'function' && successMessage) {
        queuePortalToast(successMessage, 'success');
    }
    document.location.reload(true);
}

/** Swap #list-region (or #grants-region) HTML via AJAX without full page reload. */
function refreshListRegion(options) {
    options = options || {};
    var form = document.getElementById('formList');
    var region = document.getElementById(options.regionId || 'list-region')
        || document.getElementById('grants-region')
        || document.getElementById('list-region');
    if (!form || !region) {
        if (form && !options.noFallbackSubmit) {
            form.submit();
        }
        return $.Deferred().reject().promise();
    }

    // Caller must close the entity modal first (finishPortalCrudSuccess does).
    if (!options.allowWhileModalOpen && $('#itemDetailsModal').hasClass('show')) {
        return $.Deferred().resolve().promise();
    }

    var url = options.url
        || form.dataset.listPartialUrl
        || form.dataset.pagePartialUrl
        || getListPartialUrl();
    var seq = ++portalListRefreshSeq;
    setPortalListRegionLoading(region, true);

    return $.ajax({
        type: 'POST',
        url: url,
        data: $(form).serialize(),
        headers: { 'X-Requested-With': 'XMLHttpRequest' }
    }).done(function (html) {
        if (seq !== portalListRefreshSeq) {
            return;
        }
        if (typeof html === 'string' && isPortalLoginHtml(html)) {
            window.location = '/Login/Index';
            return;
        }
        region.innerHTML = html;
        if (typeof window.portalListInit === 'function') {
            window.portalListInit();
        }
        if (typeof $ !== 'undefined' && $.fn.lazyload) {
            $('img.lazyload', region).lazyload();
        }
        if (typeof options.afterSwap === 'function') {
            options.afterSwap(region);
        }
        if (typeof window.onPortalRegionSwapped === 'function') {
            window.onPortalRegionSwapped(region);
        }
    }).fail(function (xhr) {
        if (typeof handlePortalAjaxError === 'function' && handlePortalAjaxError(xhr)) {
            return;
        }
        if (options.noFallbackSubmit) {
            var msg = 'Query failed. Please try again.';
            if (xhr && xhr.status === 404) {
                msg = 'No record found for the given parameters.';
            } else if (xhr && xhr.status === 400) {
                msg = 'Invalid query parameters.';
            } else if (xhr && xhr.status >= 500) {
                msg = 'Server error. Please try again.';
            }
            region.innerHTML =
                '<div class="alert alert-warning" role="alert">' +
                String(msg).replace(/</g, '&lt;').replace(/>/g, '&gt;') +
                '</div>';
            return;
        }
        if (!options.noFallbackSubmit) {
            form.submit();
        }
    }).always(function () {
        if (seq === portalListRefreshSeq) {
            setPortalListRegionLoading(region, false);
        }
    });
}

window.refreshListRegion = refreshListRegion;
window.finishPortalCrudSuccess = finishPortalCrudSuccess;
window.hidePortalEntityModal = hidePortalEntityModal;

/** Bootstrap confirm before Delete modal form submit (runs before per-page AJAX handlers). */
var portalPendingDeleteForm = null;

function ensurePortalDeleteConfirmModal() {
    var existing = document.getElementById('portalDeleteConfirmModal');
    // Drop older markup (entity-styled or missing undo warning) so latest copy is used.
    if (existing && (existing.querySelector('.portal-entity-modal')
        || !(existing.textContent || '').includes('cannot be undone'))) {
        existing.remove();
        existing = null;
    }
    if (existing)
        return existing;

    var wrap = document.createElement('div');
    wrap.innerHTML =
        '<div class="modal fade" id="portalDeleteConfirmModal" tabindex="-1" role="dialog" aria-labelledby="portalDeleteConfirmTitle" aria-hidden="true">' +
        '  <div class="modal-dialog" role="document">' +
        '    <div class="modal-content">' +
        '      <div class="modal-header">' +
        '        <h5 class="modal-title" id="portalDeleteConfirmTitle">Confirm delete</h5>' +
        '        <button class="btn-close" type="button" data-bs-dismiss="modal" aria-label="Close"></button>' +
        '      </div>' +
        '      <div class="modal-body">' +
        '        <p class="mb-2">Select "Delete" below if you are sure you want to remove this record.</p>' +
        '        <p class="mb-0 small text-muted">This action cannot be undone.</p>' +
        '      </div>' +
        '      <div class="modal-footer">' +
        '        <button class="btn btn-secondary" type="button" data-bs-dismiss="modal">Cancel</button>' +
        '        <button class="btn btn-danger" type="button" id="portalDeleteConfirmOk">Delete</button>' +
        '      </div>' +
        '    </div>' +
        '  </div>' +
        '</div>';
    var modalEl = wrap.firstElementChild;
    document.body.appendChild(modalEl);

    modalEl.querySelector('#portalDeleteConfirmOk').addEventListener('click', function () {
        var form = portalPendingDeleteForm;
        portalPendingDeleteForm = null;
        var instance = (typeof bootstrap !== 'undefined')
            ? bootstrap.Modal.getInstance(modalEl)
            : null;
        if (instance)
            instance.hide();
        else
            $(modalEl).modal('hide');
        if (!form)
            return;
        form.dataset.portalDeleteConfirmed = '1';
        if (typeof $ === 'function')
            $(form).trigger('submit');
        else if (typeof form.requestSubmit === 'function')
            form.requestSubmit();
        else
            form.dispatchEvent(new Event('submit', { bubbles: true, cancelable: true }));
    });

    $(modalEl).on('hidden.bs.modal', function () {
        portalPendingDeleteForm = null;
    });

    return modalEl;
}

function showPortalDeleteConfirm() {
    var modalEl = ensurePortalDeleteConfirmModal();
    if (typeof bootstrap !== 'undefined') {
        bootstrap.Modal.getOrCreateInstance(modalEl).show();
    } else {
        $(modalEl).modal('show');
    }
}

document.addEventListener('submit', function (e) {
    var form = e.target;
    if (!form || form.tagName !== 'FORM')
        return;

    // Query / list pages: keep the shell in place and swap #list-region via AJAX.
    if (form.id === 'formList'
        && document.getElementById('list-region')
        && (form.dataset.listPartialUrl || form.dataset.pagePartialUrl)
        && typeof refreshListRegion === 'function') {
        e.preventDefault();
        e.stopImmediatePropagation();
        var hasRun = document.getElementById('HasRun');
        if (hasRun) {
            hasRun.value = 'true';
        }
        refreshListRegion({ noFallbackSubmit: true });
        return;
    }

    var isDeleteForm = form.id === 'formDelete'
        || (form.classList && form.classList.contains('portal-entity-form')
            && form.closest('.portal-entity-modal[data-operation="Delete"]'));
    if (!isDeleteForm)
        return;
    if (form.dataset.portalDeleteConfirmed === '1') {
        delete form.dataset.portalDeleteConfirmed;
        return;
    }
    e.preventDefault();
    e.stopImmediatePropagation();
    portalPendingDeleteForm = form;
    showPortalDeleteConfirm();
}, true);

/** List CRUD (Detail/Update/Delete/Insert): full-viewport busy veil until modal HTML arrives. */
function showPortalCrudBusy(triggerBtn) {
    document.body.classList.add('portal-crud-busy-open');
    if (!$('body > .portal-crud-busy').length) {
        $('body').append(
            '<div class="portal-crud-busy" role="status" aria-live="polite" aria-busy="true">' +
            '<div class="portal-crud-busy__panel">' +
            '<span class="portal-crud-busy__spinner" aria-hidden="true"></span>' +
            '<span class="portal-crud-busy__label">Loading…</span>' +
            '</div></div>'
        );
    }
    if (triggerBtn) {
        var $btn = $(triggerBtn);
        $btn.addClass('is-busy').prop('disabled', true).attr('aria-busy', 'true');
        var $icon = $btn.find('i').first();
        if ($icon.length && !$icon.data('portal-busy-icon')) {
            $icon.data('portal-busy-icon', $icon.attr('class'));
            $icon.attr('class', 'fas fa-spinner fa-spin');
        }
    }
    $('.opButtonDetail, .opButtonUpdate, .opButtonDelete, .opButtonInsert')
        .not(triggerBtn)
        .prop('disabled', true);
}

function hidePortalCrudBusy(triggerBtn) {
    document.body.classList.remove('portal-crud-busy-open');
    $('body > .portal-crud-busy').remove();
    $('.opButtonDetail, .opButtonUpdate, .opButtonDelete, .opButtonInsert')
        .prop('disabled', false)
        .removeClass('is-busy')
        .removeAttr('aria-busy');
    if (triggerBtn) {
        var $btn = $(triggerBtn);
        var $icon = $btn.find('i').first();
        var prev = $icon.data('portal-busy-icon');
        if (prev) {
            $icon.attr('class', prev);
            $icon.removeData('portal-busy-icon');
        }
    }
}

function enhancePortalEntityForm(root) {
    if (!root)
        return;

    const form = root.querySelector('form.needs-validation, form.portal-entity-form');
    if (form && !form.querySelector('.portal-entity-actions') && !form.querySelector('.portal-entity-footer')) {
        // Collect trailing buttons after fields (Close / Update / Delete / Insert / Clear)
        const trailing = [];
        for (let i = form.children.length - 1; i >= 0; i--) {
            const el = form.children[i];
            if (el.id === 'actionResultDiv')
                continue;
            if (el.matches && el.matches('hr')) {
                el.remove();
                continue;
            }
            const isAction = el.matches && (
                el.matches('button, .btn, a.btn') ||
                (el.classList && (el.classList.contains('float-left') || el.classList.contains('float-right')))
            );
            if (isAction) {
                trailing.unshift(el);
                continue;
            }
            break;
        }

        if (trailing.length) {
            const wrap = document.createElement('div');
            wrap.className = 'portal-entity-footer';
            const actions = document.createElement('div');
            actions.className = 'portal-entity-actions';
            trailing.forEach(function (el) {
                el.classList.remove('float-left', 'float-right');
                actions.appendChild(el);
            });
            wrap.appendChild(actions);
            form.appendChild(wrap);
        }
    }

    pinPortalEntityFooter(root);

    const title = root.querySelector('.modal-title');
    const modalContent = root.querySelector('.modal-content') || root.closest('.modal-content');
    if (title && modalContent) {
        const t = (title.textContent || '').toLowerCase();
        if (t.includes('delete'))
            modalContent.setAttribute('data-operation', 'Delete');
        else if (t.includes('update') || t.includes('edit'))
            modalContent.setAttribute('data-operation', 'Update');
        else if (t.includes('insert') || t.includes('create'))
            modalContent.setAttribute('data-operation', 'Insert');
        else
            modalContent.setAttribute('data-operation', 'Detail');
    }
}

/**
 * Move the action footer out of the scrolling .modal-body so content cannot
 * scroll underneath it. Buttons keep working via the HTML form= attribute.
 */
function pinPortalEntityFooter(root) {
    if (!root)
        return;

    const modalContent = root.querySelector('.modal-content') || root.closest('.modal-content');
    if (!modalContent)
        return;

    const form = root.querySelector('form.needs-validation, form.portal-entity-form')
        || modalContent.querySelector('form.needs-validation, form.portal-entity-form');
    let footer = root.querySelector('.portal-entity-footer')
        || modalContent.querySelector('.portal-entity-footer');

    // Upgrade bare .portal-entity-actions into a footer wrapper
    if (!footer) {
        const actions = (form && form.querySelector(':scope > .portal-entity-actions'))
            || modalContent.querySelector('.modal-body .portal-entity-actions');
        if (!actions)
            return;
        footer = document.createElement('div');
        footer.className = 'portal-entity-footer';
        actions.parentNode.insertBefore(footer, actions);
        footer.appendChild(actions);
    }

    // Already pinned as a direct child of modal-content (sibling of modal-body)
    if (footer.parentElement === modalContent)
        return;

    if (form && form.id) {
        footer.querySelectorAll('button, input[type="submit"], input[type="reset"]').forEach(function (btn) {
            if (!btn.getAttribute('form'))
                btn.setAttribute('form', form.id);
            btn.classList.remove('float-left', 'float-right');
        });
    }

    modalContent.appendChild(footer);
}

var PORTAL_TOAST_STORAGE_KEY = 'portalToastMessage';

function getPortalAjaxErrorPayload(xhr) {
    if (!xhr) {
        return null;
    }
    if (xhr.responseJSON) {
        return xhr.responseJSON;
    }
    if (!xhr.responseText) {
        return null;
    }
    try {
        return JSON.parse(xhr.responseText);
    } catch (e) {
        return null;
    }
}

/** True when an AJAX response body is the portal login page (e.g. cookie 302 followed by XHR). */
function isPortalLoginHtml(data) {
    if (typeof data !== 'string' || !data) {
        return false;
    }
    return data.indexOf('auth-card') !== -1
        && (data.indexOf('auth-form') !== -1 || data.indexOf('login-password') !== -1);
}

/**
 * Shared portal AJAX failure handling.
 * Session expiry / unauthorized → login. Other errors → toast (+ optional page reload).
 * @returns {boolean} true when redirected to login
 */
function handlePortalAjaxError(xhr, options) {
    options = options || {};
    if (xhr && xhr.portalErrorHandled) {
        return !!xhr.portalAuthRedirect;
    }
    if (xhr) {
        xhr.portalErrorHandled = true;
    }

    var payload = getPortalAjaxErrorPayload(xhr);
    var redirectUrl = (payload && payload.redirectUrl) || null;
    var responseText = xhr && typeof xhr.responseText === 'string' ? xhr.responseText : '';
    // 401 (legacy) or 403+redirectUrl (session expiry — avoids native browser auth prompts)
    var isAuth = (xhr && xhr.status === 401)
        || !!redirectUrl
        || (payload && payload.statusCode === 401)
        || isPortalLoginHtml(responseText);

    if (isAuth) {
        if (xhr) {
            xhr.portalAuthRedirect = true;
        }
        window.location = redirectUrl || '/Login/Index';
        return true;
    }

    var msg = (payload && payload.message)
        || options.fallbackMessage
        || (xhr && xhr.statusText)
        || 'Request failed';

    if (options.reload) {
        queuePortalToast(msg, 'error');
        document.location.reload(true);
    } else if (typeof showPortalToast === 'function') {
        showPortalToast(msg, 'error');
    }

    return false;
}

function showPortalToast(message, type) {
    if (!message) {
        return;
    }

    var toastEl = document.getElementById('portalToast');
    var toastBody = document.getElementById('portalToastBody');
    var toastIcon = document.getElementById('portalToastIcon');
    if (!toastEl || !toastBody) {
        return;
    }

    var toastType = (type || 'success').toLowerCase();
    toastEl.classList.remove('text-bg-success', 'text-bg-danger', 'text-bg-warning', 'text-bg-info');

    var iconClass = 'fas fa-check-circle';
    if (toastType === 'error' || toastType === 'danger') {
        toastEl.classList.add('text-bg-danger');
        iconClass = 'fas fa-exclamation-circle';
    } else if (toastType === 'warning') {
        toastEl.classList.add('text-bg-warning');
        iconClass = 'fas fa-exclamation-triangle';
    } else if (toastType === 'info') {
        toastEl.classList.add('text-bg-info');
        iconClass = 'fas fa-info-circle';
    } else {
        toastEl.classList.add('text-bg-success');
        iconClass = 'fas fa-check-circle';
    }

    if (toastIcon) {
        toastIcon.className = iconClass;
    }

    toastBody.textContent = message;

    if (typeof bootstrap !== 'undefined' && bootstrap.Toast) {
        var toast = bootstrap.Toast.getOrCreateInstance(toastEl, { delay: 3500, autohide: true });
        toast.show();
    } else if (typeof $.fn.toast !== 'undefined') {
        $(toastEl).toast({ delay: 3500, autohide: true }).toast('show');
    }
}

function queuePortalToast(message, type) {
    if (!message || typeof sessionStorage === 'undefined') {
        return;
    }

    try {
        sessionStorage.setItem(PORTAL_TOAST_STORAGE_KEY, JSON.stringify({
            message: message,
            type: type || 'success'
        }));
    } catch (e) {
        // Ignore storage failures (private mode / quota).
    }
}

function consumeQueuedPortalToast() {
    if (typeof sessionStorage === 'undefined') {
        return;
    }

    try {
        var raw = sessionStorage.getItem(PORTAL_TOAST_STORAGE_KEY);
        if (!raw) {
            return;
        }
        sessionStorage.removeItem(PORTAL_TOAST_STORAGE_KEY);
        var payload = JSON.parse(raw);
        if (payload && payload.message) {
            showPortalToast(payload.message, payload.type || 'success');
        }
    } catch (e) {
        sessionStorage.removeItem(PORTAL_TOAST_STORAGE_KEY);
    }
}

function getPortalCrudSuccessMessage(operation) {
    switch ((operation || '').toLowerCase()) {
        case 'update':
            return 'Record updated successfully.';
        case 'delete':
            return 'Record deleted successfully.';
        case 'insert':
            return 'Record created successfully.';
        default:
            return 'Operation completed successfully.';
    }
}

function init() {
    // Bootstrap 5 popover initialization
    if (typeof bootstrap !== 'undefined') {
        const popoverTriggerList = document.querySelectorAll('[data-bs-toggle="popover"]');
        [...popoverTriggerList].map(popoverTriggerEl => new bootstrap.Popover(popoverTriggerEl));
        // Also support legacy data-toggle for backward compatibility
        const legacyPopoverList = document.querySelectorAll('[data-toggle="popover"]');
        [...legacyPopoverList].map(popoverTriggerEl => new bootstrap.Popover(popoverTriggerEl));
    } else if (typeof $.fn.popover !== 'undefined') {
        // Fallback for Bootstrap 4 or jQuery popover
        $('[data-toggle="popover"]').popover();
        $('[data-bs-toggle="popover"]').popover();
    }
    consumeQueuedPortalToast();
    const placeholderElement = $('#itemDetailsContainer');
    // Select2 first — before Flatpickr — so combos do not sit unenhanced for long.
    initSearchableSelects(document);
    initFlatpickrModalFix();
    initDatePickers(document);

    $('button[data-toggle="ajax-modal"]').click(function (event) {
        let url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            // Bootstrap 5 compatible modal show
            var modalElement = placeholderElement.find('.modal')[0];
            if (modalElement && typeof bootstrap !== 'undefined') {
                var modal = new bootstrap.Modal(modalElement);
                modal.show();
            } else {
                placeholderElement.find('.modal').modal('show');
            }
            initDatePickers(placeholderElement[0]);
        });
    });

    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();

        let form = $(this).parents('.modal').find('form');
        let actionUrl = form.attr('action');
        let dataToSend = form.serialize();

        $.post(actionUrl, dataToSend).done(function (data) {
            let isValid = placeholderElement.find('[name="IsValid"]').val() === 'True';
            if (isValid) {
                // Bootstrap 5 compatible modal hide
                var modalElement = placeholderElement.find('.modal')[0];
                if (modalElement && typeof bootstrap !== 'undefined') {
                    var modal = bootstrap.Modal.getInstance(modalElement);
                    if (modal) {
                        modal.hide();
                    }
                } else {
                    placeholderElement.find('.modal').modal('hide');
                }
            }
        });
    });

    // Immersive image / PDF / video lightbox (BS5-safe; replaces ekko-lightbox).
    $(document).on('click', '[data-toggle="lightbox"]', function (event) {
        event.preventDefault();
        var href = $(this).attr('href');
        if (!href) {
            return;
        }
        var type = (($(this).attr('data-type') || 'image') + '').toLowerCase();
        if (type === 'pdf') {
            openPortalPdfLightbox(href);
            return;
        }
        if (type === 'video') {
            openPortalVideoLightbox(href);
            return;
        }
        openPortalImageLightbox(href);
    });

    // Client-side max size for BLOB_* / IMAGE uploads (server still enforces).
    $(document).on('change', 'input[type="file"][data-max-bytes]', function () {
        var input = this;
        var maxBytes = parseInt(input.getAttribute('data-max-bytes') || '0', 10);
        if (!maxBytes || !input.files || !input.files.length) {
            return;
        }
        var file = input.files[0];
        if (file.size <= maxBytes) {
            return;
        }
        var maxMb = Math.round(maxBytes / (1024 * 1024));
        var sizeMb = (file.size / (1024 * 1024)).toFixed(1);
        input.value = '';
        window.alert('File is too large (' + sizeMb + ' MB). Maximum allowed is ' + maxMb + ' MB.');
    });

    bindPortalModalStack();
}

// Nested modals (e.g. Kafka workflow list → detail): BS keeps every backdrop at 1040,
// so only the first blur shows. Raise each modal/backdrop pair so the top veil covers the one below.
var portalModalStackBound = false;

function syncPortalModalStack(openingModal) {
    var openModals = Array.prototype.slice.call(document.querySelectorAll('.modal.show'));
    if (openingModal && openModals.indexOf(openingModal) === -1) {
        openModals.push(openingModal);
    }

    var backdrops = Array.prototype.slice.call(document.querySelectorAll('body > .modal-backdrop'));

    openModals.forEach(function (modal, index) {
        modal.style.zIndex = String(1055 + (index * 20));
        modal.classList.toggle('portal-modal-behind', index < openModals.length - 1);
    });

    backdrops.forEach(function (backdrop, index) {
        backdrop.style.zIndex = String(1050 + (index * 20));
        backdrop.classList.toggle('portal-backdrop-nested', index > 0);
    });
}

function schedulePortalModalStackSync(openingModal) {
    syncPortalModalStack(openingModal);
    // Backdrop is inserted just after show starts — catch it before fade finishes.
    window.requestAnimationFrame(function () {
        syncPortalModalStack(openingModal);
        window.requestAnimationFrame(function () {
            syncPortalModalStack(openingModal);
        });
    });
}

function bindPortalModalStack() {
    if (portalModalStackBound) {
        return;
    }
    portalModalStackBound = true;

    $(document).on('show.bs.modal.portalStack', '.modal', function () {
        var openCount = document.querySelectorAll('.modal.show').length;
        this.style.zIndex = String(1055 + (openCount * 20));
        // Dim parents immediately — waiting for shown.bs.modal felt like a late blur.
        document.querySelectorAll('.modal.show').forEach(function (modal) {
            modal.classList.add('portal-modal-behind');
        });
        schedulePortalModalStackSync(this);
    });

    $(document).on('shown.bs.modal.portalStack', '.modal', function () {
        syncPortalModalStack();
    });

    $(document).on('hidden.bs.modal.portalStack', '.modal', function () {
        this.style.zIndex = '';
        this.classList.remove('portal-modal-behind');
        window.setTimeout(syncPortalModalStack, 10);
    });
}

var portalLightboxState = {
    scale: 1,
    x: 0,
    y: 0,
    min: 1,
    max: 6,
    dragging: false,
    moved: false,
    startX: 0,
    startY: 0,
    originX: 0,
    originY: 0,
    pointers: new Map(),
    pinchStartDist: 0,
    pinchStartScale: 1
};

function ensurePortalImageLightbox() {
    var root = document.getElementById('portalImageLightbox');
    if (root) {
        return root;
    }

    document.body.insertAdjacentHTML('beforeend',
        '<div id="portalImageLightbox" class="portal-lightbox" hidden aria-hidden="true" role="dialog" aria-modal="true" aria-label="Image preview">' +
        '  <div class="portal-lightbox__veil" data-portal-lightbox-close="1"></div>' +
        '  <button type="button" class="portal-lightbox__close" data-portal-lightbox-close="1" aria-label="Close preview">' +
        '    <svg viewBox="0 0 24 24" width="18" height="18" aria-hidden="true"><path d="M6.4 6.4l11.2 11.2M17.6 6.4L6.4 17.6" fill="none" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/></svg>' +
        '  </button>' +
        '  <div class="portal-lightbox__toolbar" role="toolbar" aria-label="Zoom controls">' +
        '    <button type="button" class="portal-lightbox__tool" data-portal-lightbox-zoom="-1" aria-label="Zoom out">' +
        '      <svg viewBox="0 0 24 24" width="16" height="16" aria-hidden="true"><path d="M5 12h14" fill="none" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/></svg>' +
        '    </button>' +
        '    <span class="portal-lightbox__zoom-label" id="portalImageLightboxZoom">100%</span>' +
        '    <button type="button" class="portal-lightbox__tool" data-portal-lightbox-zoom="1" aria-label="Zoom in">' +
        '      <svg viewBox="0 0 24 24" width="16" height="16" aria-hidden="true"><path d="M12 5v14M5 12h14" fill="none" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/></svg>' +
        '    </button>' +
        '    <button type="button" class="portal-lightbox__tool" data-portal-lightbox-zoom="reset" aria-label="Reset zoom">' +
        '      <svg viewBox="0 0 24 24" width="16" height="16" aria-hidden="true"><path d="M4.5 12a7.5 7.5 0 1 0 2.2-5.3M4.5 4.5v4h4" fill="none" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/></svg>' +
        '    </button>' +
        '  </div>' +
        '  <figure class="portal-lightbox__stage">' +
        '    <div class="portal-lightbox__frame">' +
        '      <div class="portal-lightbox__viewport">' +
        '        <img id="portalImageLightboxImg" class="portal-lightbox__img" alt="" draggable="false" />' +
        '      </div>' +
        '      <div class="portal-lightbox__shine" aria-hidden="true"></div>' +
        '    </div>' +
        '  </figure>' +
        '</div>');

    root = document.getElementById('portalImageLightbox');
    bindPortalLightboxInteractions(root);
    return root;
}

function bindPortalLightboxInteractions(root) {
    var viewport = root.querySelector('.portal-lightbox__viewport');
    var img = document.getElementById('portalImageLightboxImg');

    root.addEventListener('click', function (e) {
        var zoomEl = e.target && e.target.closest
            ? e.target.closest('[data-portal-lightbox-zoom]')
            : null;
        var zoomAction = zoomEl && zoomEl.getAttribute('data-portal-lightbox-zoom');
        if (zoomAction === '1' || zoomAction === '-1') {
            nudgePortalLightboxZoom(Number(zoomAction) > 0 ? 0.25 : -0.25);
            return;
        }
        if (zoomAction === 'reset') {
            resetPortalLightboxZoom();
            return;
        }
        // Click may land on the SVG/path inside the close button — use closest.
        if (e.target && e.target.closest && e.target.closest('[data-portal-lightbox-close="1"]')) {
            closePortalImageLightbox();
        }
    });

    document.addEventListener('keydown', function (e) {
        if (!root || root.hasAttribute('hidden')) {
            return;
        }
        if (e.key === 'Escape') {
            closePortalImageLightbox();
        } else if (e.key === '+' || e.key === '=') {
            e.preventDefault();
            nudgePortalLightboxZoom(0.25);
        } else if (e.key === '-' || e.key === '_') {
            e.preventDefault();
            nudgePortalLightboxZoom(-0.25);
        } else if (e.key === '0') {
            e.preventDefault();
            resetPortalLightboxZoom();
        }
    });

    if (!viewport || !img) {
        return;
    }

    viewport.addEventListener('wheel', function (e) {
        e.preventDefault();
        var delta = e.deltaY > 0 ? -0.12 : 0.12;
        setPortalLightboxZoom(portalLightboxState.scale + delta, e.clientX, e.clientY);
    }, { passive: false });

    viewport.addEventListener('dblclick', function (e) {
        e.preventDefault();
        if (portalLightboxState.scale > 1.05) {
            resetPortalLightboxZoom();
        } else {
            setPortalLightboxZoom(2.2, e.clientX, e.clientY);
        }
    });

    viewport.addEventListener('pointerdown', function (e) {
        if (e.button !== undefined && e.button !== 0) {
            return;
        }
        viewport.setPointerCapture(e.pointerId);
        portalLightboxState.pointers.set(e.pointerId, { x: e.clientX, y: e.clientY });
        portalLightboxState.moved = false;

        if (portalLightboxState.pointers.size === 1) {
            // Pan only when zoomed content overflows the viewport.
            portalLightboxState.dragging = portalLightboxCanPan();
            portalLightboxState.startX = e.clientX;
            portalLightboxState.startY = e.clientY;
            portalLightboxState.originX = portalLightboxState.x;
            portalLightboxState.originY = portalLightboxState.y;
            viewport.classList.toggle('is-dragging', portalLightboxState.dragging);
        } else if (portalLightboxState.pointers.size === 2) {
            portalLightboxState.dragging = false;
            var pts = Array.from(portalLightboxState.pointers.values());
            portalLightboxState.pinchStartDist = Math.hypot(pts[0].x - pts[1].x, pts[0].y - pts[1].y);
            portalLightboxState.pinchStartScale = portalLightboxState.scale;
        }
    });

    viewport.addEventListener('pointermove', function (e) {
        if (!portalLightboxState.pointers.has(e.pointerId)) {
            return;
        }
        portalLightboxState.pointers.set(e.pointerId, { x: e.clientX, y: e.clientY });

        if (portalLightboxState.pointers.size === 2) {
            var pts = Array.from(portalLightboxState.pointers.values());
            var dist = Math.hypot(pts[0].x - pts[1].x, pts[0].y - pts[1].y);
            if (portalLightboxState.pinchStartDist > 0) {
                var next = portalLightboxState.pinchStartScale * (dist / portalLightboxState.pinchStartDist);
                var midX = (pts[0].x + pts[1].x) / 2;
                var midY = (pts[0].y + pts[1].y) / 2;
                setPortalLightboxZoom(next, midX, midY);
            }
            return;
        }

        if (!portalLightboxState.dragging) {
            return;
        }
        var dx = e.clientX - portalLightboxState.startX;
        var dy = e.clientY - portalLightboxState.startY;
        if (Math.abs(dx) + Math.abs(dy) > 3) {
            portalLightboxState.moved = true;
        }
        portalLightboxState.x = portalLightboxState.originX + dx;
        portalLightboxState.y = portalLightboxState.originY + dy;
        applyPortalLightboxTransform();
    });

    function endPointer(e) {
        if (portalLightboxState.pointers.has(e.pointerId)) {
            portalLightboxState.pointers.delete(e.pointerId);
        }
        if (portalLightboxState.pointers.size < 2) {
            portalLightboxState.pinchStartDist = 0;
        }
        if (portalLightboxState.pointers.size === 0) {
            portalLightboxState.dragging = false;
            viewport.classList.remove('is-dragging');
        }
    }

    viewport.addEventListener('pointerup', endPointer);
    viewport.addEventListener('pointercancel', endPointer);
    viewport.addEventListener('lostpointercapture', endPointer);
}

function getPortalLightboxPanLimits() {
    var img = document.getElementById('portalImageLightboxImg');
    var viewport = document.querySelector('#portalImageLightbox .portal-lightbox__viewport');
    if (!img || !viewport) {
        return { maxX: 0, maxY: 0 };
    }

    var scaledW = img.offsetWidth * portalLightboxState.scale;
    var scaledH = img.offsetHeight * portalLightboxState.scale;
    return {
        maxX: Math.max(0, (scaledW - viewport.clientWidth) / 2),
        maxY: Math.max(0, (scaledH - viewport.clientHeight) / 2)
    };
}

function clampPortalLightboxPan() {
    var limits = getPortalLightboxPanLimits();
    portalLightboxState.x = Math.min(limits.maxX, Math.max(-limits.maxX, portalLightboxState.x));
    portalLightboxState.y = Math.min(limits.maxY, Math.max(-limits.maxY, portalLightboxState.y));
    return limits;
}

function portalLightboxCanPan() {
    var limits = getPortalLightboxPanLimits();
    return limits.maxX > 0.5 || limits.maxY > 0.5;
}

function applyPortalLightboxTransform() {
    var img = document.getElementById('portalImageLightboxImg');
    var root = document.getElementById('portalImageLightbox');
    var label = document.getElementById('portalImageLightboxZoom');
    if (!img) {
        return;
    }

    clampPortalLightboxPan();
    img.style.transform = 'translate(' + portalLightboxState.x + 'px, ' + portalLightboxState.y + 'px) scale(' + portalLightboxState.scale + ')';
    if (label) {
        label.textContent = Math.round(portalLightboxState.scale * 100) + '%';
    }
    if (root) {
        // Grab cursor only when there is overflow to pan inside the viewport.
        root.classList.toggle('is-zoomed', portalLightboxCanPan());
    }
}

function setPortalLightboxZoom(nextScale, clientX, clientY) {
    var img = document.getElementById('portalImageLightboxImg');
    var viewport = document.querySelector('#portalImageLightbox .portal-lightbox__viewport');
    if (!img || !viewport) {
        return;
    }

    var prev = portalLightboxState.scale;
    var next = Math.min(portalLightboxState.max, Math.max(portalLightboxState.min, nextScale));
    if (Math.abs(next - prev) < 0.001) {
        applyPortalLightboxTransform();
        return;
    }

    if (typeof clientX === 'number' && typeof clientY === 'number') {
        var rect = viewport.getBoundingClientRect();
        var cx = clientX - rect.left - rect.width / 2;
        var cy = clientY - rect.top - rect.height / 2;
        var ratio = next / prev;
        portalLightboxState.x = cx - (cx - portalLightboxState.x) * ratio;
        portalLightboxState.y = cy - (cy - portalLightboxState.y) * ratio;
    }

    portalLightboxState.scale = next;
    if (next <= 1.001) {
        portalLightboxState.x = 0;
        portalLightboxState.y = 0;
        portalLightboxState.scale = 1;
    }
    applyPortalLightboxTransform();
}

function nudgePortalLightboxZoom(delta) {
    setPortalLightboxZoom(portalLightboxState.scale + delta);
}

function resetPortalLightboxZoom() {
    portalLightboxState.scale = 1;
    portalLightboxState.x = 0;
    portalLightboxState.y = 0;
    applyPortalLightboxTransform();
}

function openPortalImageLightbox(href) {
    var root = ensurePortalImageLightbox();
    var img = document.getElementById('portalImageLightboxImg');
    var frame = root.querySelector('.portal-lightbox__frame');

    if (typeof closePortalPdfLightbox === 'function') {
        closePortalPdfLightbox();
    }
    if (typeof closePortalVideoLightbox === 'function') {
        closePortalVideoLightbox();
    }

    resetPortalLightboxZoom();
    root.removeAttribute('hidden');
    root.setAttribute('aria-hidden', 'false');
    document.documentElement.classList.add('portal-lightbox-open');
    document.body.classList.add('portal-lightbox-open');

    if (frame) {
        frame.classList.remove('is-ready');
        frame.classList.add('is-loading');
    }

    img.onload = function () {
        if (frame) {
            frame.classList.remove('is-loading');
            frame.classList.add('is-ready');
        }
        resetPortalLightboxZoom();
    };
    img.onerror = function () {
        if (frame) {
            frame.classList.remove('is-loading');
            frame.classList.add('is-ready');
        }
    };

    void root.offsetWidth;
    root.classList.add('is-open');
    img.alt = 'Preview';
    img.src = href;

    var closeBtn = root.querySelector('.portal-lightbox__close');
    if (closeBtn) {
        closeBtn.focus({ preventScroll: true });
    }
}

function isPortalLightboxVisible(id) {
    var el = document.getElementById(id);
    return !!(el && !el.hasAttribute('hidden'));
}

function releasePortalLightboxBodyLock() {
    if (isPortalLightboxVisible('portalImageLightbox')
        || isPortalLightboxVisible('portalPdfLightbox')
        || isPortalLightboxVisible('portalVideoLightbox')) {
        return;
    }
    document.documentElement.classList.remove('portal-lightbox-open');
    document.body.classList.remove('portal-lightbox-open');
    if ($('#itemDetailsModal').hasClass('show')) {
        $('body').addClass('modal-open');
    }
}

function closePortalImageLightbox() {
    var root = document.getElementById('portalImageLightbox');
    if (!root || root.hasAttribute('hidden')) {
        return;
    }

    root.classList.remove('is-open');
    root.setAttribute('aria-hidden', 'true');

    window.setTimeout(function () {
        var img = document.getElementById('portalImageLightboxImg');
        if (img) {
            img.removeAttribute('src');
            img.alt = '';
            img.style.transform = '';
        }
        resetPortalLightboxZoom();
        root.setAttribute('hidden', '');
        releasePortalLightboxBodyLock();
    }, 220);
}

function ensurePortalPdfLightbox() {
    var root = document.getElementById('portalPdfLightbox');
    if (root) {
        return root;
    }

    document.body.insertAdjacentHTML('beforeend',
        '<div id="portalPdfLightbox" class="portal-lightbox portal-lightbox--pdf" hidden aria-hidden="true" role="dialog" aria-modal="true" aria-label="PDF preview">' +
        '  <div class="portal-lightbox__veil" data-portal-pdf-close="1"></div>' +
        '  <button type="button" class="portal-lightbox__close" data-portal-pdf-close="1" aria-label="Close preview">' +
        '    <svg viewBox="0 0 24 24" width="18" height="18" aria-hidden="true"><path d="M6.4 6.4l11.2 11.2M17.6 6.4L6.4 17.6" fill="none" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/></svg>' +
        '  </button>' +
        '  <div class="portal-lightbox__toolbar portal-lightbox__toolbar--pdf" role="toolbar" aria-label="PDF controls">' +
        '    <a id="portalPdfLightboxOpen" class="portal-lightbox__tool portal-lightbox__tool-link" href="#" target="_blank" rel="noopener noreferrer">Open in new tab</a>' +
        '  </div>' +
        '  <figure class="portal-lightbox__stage portal-lightbox__stage--pdf">' +
        '    <div class="portal-lightbox__frame portal-lightbox__frame--pdf is-loading">' +
        '      <iframe id="portalPdfLightboxFrame" class="portal-lightbox__pdf" title="PDF preview" loading="lazy"></iframe>' +
        '    </div>' +
        '  </figure>' +
        '</div>');

    root = document.getElementById('portalPdfLightbox');
    root.addEventListener('click', function (e) {
        if (e.target && e.target.closest && e.target.closest('[data-portal-pdf-close="1"]')) {
            closePortalPdfLightbox();
        }
    });
    document.addEventListener('keydown', function (e) {
        if (!root || root.hasAttribute('hidden')) {
            return;
        }
        if (e.key === 'Escape') {
            closePortalPdfLightbox();
        }
    });
    return root;
}

function openPortalPdfLightbox(href) {
    var root = ensurePortalPdfLightbox();
    var frame = document.getElementById('portalPdfLightboxFrame');
    var openLink = document.getElementById('portalPdfLightboxOpen');
    var shell = root.querySelector('.portal-lightbox__frame--pdf');

    closePortalImageLightbox();
    if (typeof closePortalVideoLightbox === 'function') {
        closePortalVideoLightbox();
    }

    if (openLink) {
        openLink.href = href;
    }
    if (shell) {
        shell.classList.add('is-loading');
        shell.classList.remove('is-ready');
    }

    root.removeAttribute('hidden');
    root.setAttribute('aria-hidden', 'false');
    document.documentElement.classList.add('portal-lightbox-open');
    document.body.classList.add('portal-lightbox-open');

    frame.onload = function () {
        if (shell) {
            shell.classList.remove('is-loading');
            shell.classList.add('is-ready');
        }
    };
    frame.onerror = function () {
        if (shell) {
            shell.classList.remove('is-loading');
            shell.classList.add('is-ready');
        }
    };

    // Some browsers need #toolbar=0 hints; keep source clean for public blob URLs.
    frame.src = href;

    void root.offsetWidth;
    root.classList.add('is-open');

    var closeBtn = root.querySelector('.portal-lightbox__close');
    if (closeBtn) {
        closeBtn.focus({ preventScroll: true });
    }
}

function closePortalPdfLightbox() {
    var root = document.getElementById('portalPdfLightbox');
    if (!root || root.hasAttribute('hidden')) {
        return;
    }

    root.classList.remove('is-open');
    root.setAttribute('aria-hidden', 'true');

    window.setTimeout(function () {
        var frame = document.getElementById('portalPdfLightboxFrame');
        if (frame) {
            frame.removeAttribute('src');
        }
        var shell = root.querySelector('.portal-lightbox__frame--pdf');
        if (shell) {
            shell.classList.remove('is-ready');
            shell.classList.add('is-loading');
        }
        root.setAttribute('hidden', '');
        releasePortalLightboxBodyLock();
    }, 220);
}

function ensurePortalVideoLightbox() {
    var root = document.getElementById('portalVideoLightbox');
    if (root) {
        return root;
    }

    document.body.insertAdjacentHTML('beforeend',
        '<div id="portalVideoLightbox" class="portal-lightbox portal-lightbox--video" hidden aria-hidden="true" role="dialog" aria-modal="true" aria-label="Video preview">' +
        '  <div class="portal-lightbox__veil" data-portal-video-close="1"></div>' +
        '  <button type="button" class="portal-lightbox__close" data-portal-video-close="1" aria-label="Close preview">' +
        '    <svg viewBox="0 0 24 24" width="18" height="18" aria-hidden="true"><path d="M6.4 6.4l11.2 11.2M17.6 6.4L6.4 17.6" fill="none" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/></svg>' +
        '  </button>' +
        '  <div class="portal-lightbox__toolbar portal-lightbox__toolbar--video" role="toolbar" aria-label="Video controls">' +
        '    <a id="portalVideoLightboxOpen" class="portal-lightbox__tool portal-lightbox__tool-link" href="#" target="_blank" rel="noopener noreferrer">Open in new tab</a>' +
        '  </div>' +
        '  <figure class="portal-lightbox__stage portal-lightbox__stage--video">' +
        '    <div class="portal-lightbox__frame portal-lightbox__frame--video is-loading">' +
        '      <video id="portalVideoLightboxPlayer" class="portal-lightbox__video" controls playsinline preload="metadata"></video>' +
        '    </div>' +
        '  </figure>' +
        '</div>');

    root = document.getElementById('portalVideoLightbox');
    root.addEventListener('click', function (e) {
        if (e.target && e.target.closest && e.target.closest('[data-portal-video-close="1"]')) {
            closePortalVideoLightbox();
        }
    });
    document.addEventListener('keydown', function (e) {
        if (!root || root.hasAttribute('hidden')) {
            return;
        }
        if (e.key === 'Escape') {
            closePortalVideoLightbox();
        }
    });
    return root;
}

function openPortalVideoLightbox(href) {
    var root = ensurePortalVideoLightbox();
    var player = document.getElementById('portalVideoLightboxPlayer');
    var openLink = document.getElementById('portalVideoLightboxOpen');
    var shell = root.querySelector('.portal-lightbox__frame--video');

    closePortalImageLightbox();
    closePortalPdfLightbox();

    if (openLink) {
        openLink.href = href;
    }
    if (shell) {
        shell.classList.add('is-loading');
        shell.classList.remove('is-ready');
    }

    root.removeAttribute('hidden');
    root.setAttribute('aria-hidden', 'false');
    document.documentElement.classList.add('portal-lightbox-open');
    document.body.classList.add('portal-lightbox-open');

    player.onloadeddata = function () {
        if (shell) {
            shell.classList.remove('is-loading');
            shell.classList.add('is-ready');
        }
    };
    player.onerror = function () {
        if (shell) {
            shell.classList.remove('is-loading');
            shell.classList.add('is-ready');
        }
    };

    player.pause();
    player.removeAttribute('src');
    player.load();
    player.src = href;
    player.load();

    void root.offsetWidth;
    root.classList.add('is-open');

    var closeBtn = root.querySelector('.portal-lightbox__close');
    if (closeBtn) {
        closeBtn.focus({ preventScroll: true });
    }
}

function closePortalVideoLightbox() {
    var root = document.getElementById('portalVideoLightbox');
    if (!root || root.hasAttribute('hidden')) {
        return;
    }

    root.classList.remove('is-open');
    root.setAttribute('aria-hidden', 'true');

    var player = document.getElementById('portalVideoLightboxPlayer');
    if (player) {
        try {
            player.pause();
        } catch (e) { /* ignore */ }
    }

    window.setTimeout(function () {
        if (player) {
            player.removeAttribute('src');
            player.load();
        }
        var shell = root.querySelector('.portal-lightbox__frame--video');
        if (shell) {
            shell.classList.remove('is-ready');
            shell.classList.add('is-loading');
        }
        root.setAttribute('hidden', '');
        releasePortalLightboxBodyLock();
    }, 220);
}

function initFlatpickrModalFix() {
    if (window._flatpickrModalFixInitialized) {
        return;
    }
    window._flatpickrModalFixInitialized = true;
    document.addEventListener('focusin', function (e) {
        if (e.target.closest && e.target.closest('.flatpickr-calendar')) {
            e.stopImmediatePropagation();
        }
    });
}

function isSentinelDateValue(value) {
    if (!value || !String(value).trim()) {
        return true;
    }

    var text = String(value).trim();
    if (/0001/.test(text)) {
        return true;
    }

    var parsed = Date.parse(text);
    if (!isNaN(parsed)) {
        return new Date(parsed).getFullYear() <= 1;
    }

    return false;
}

function formatPortalDateTime(date) {
    var d = date instanceof Date ? date : new Date();
    var pad = function (n) { return n < 10 ? '0' + n : String(n); };
    return pad(d.getDate()) + '.' + pad(d.getMonth() + 1) + '.' + d.getFullYear()
        + ' ' + pad(d.getHours()) + ':' + pad(d.getMinutes());
}

function formatPortalDateTimeIso(date) {
    var d = date instanceof Date ? date : new Date();
    var pad = function (n) { return n < 10 ? '0' + n : String(n); };
    return d.getFullYear() + '-' + pad(d.getMonth() + 1) + '-' + pad(d.getDate())
        + 'T' + pad(d.getHours()) + ':' + pad(d.getMinutes()) + ':00';
}

function isPortalInsertContext(el) {
    return !!(el && el.closest && el.closest('.portal-entity-modal[data-operation="Insert"]'));
}

function normalizePortalDateTimeFormData(formData) {
    if (!formData || typeof formData.keys !== 'function') {
        return;
    }

    var keys = Array.from(formData.keys());
    keys.forEach(function (key) {
        var value = formData.get(key);
        if (typeof value !== 'string') {
            return;
        }

        var escaped = (typeof CSS !== 'undefined' && CSS.escape)
            ? CSS.escape(key)
            : key.replace(/"/g, '\\"');
        var input = document.querySelector(
            'input.portal-datetime-input[name="' + escaped + '"], .flatpickr-datetime input[name="' + escaped + '"]'
        );
        if (!input) {
            return;
        }

        if (isSentinelDateValue(value)) {
            var now = new Date();
            var displayValue = formatPortalDateTime(now);
            formData.set(key, formatPortalDateTimeIso(now));
            input.value = displayValue;
            var wrap = input.closest('.flatpickr-datetime');
            if (wrap && wrap._flatpickr) {
                wrap._flatpickr.setDate(displayValue, false);
            }
            return;
        }

        // Convert flatpickr display values (d.m.Y H:i) to ISO for reliable server binding.
        var parts = String(value).trim().match(/^(\d{1,2})\.(\d{1,2})\.(\d{4})\s+(\d{1,2}):(\d{2})$/);
        if (parts) {
            var day = parts[1].padStart(2, '0');
            var month = parts[2].padStart(2, '0');
            var year = parts[3];
            var hour = parts[4].padStart(2, '0');
            var minute = parts[5];
            formData.set(key, year + '-' + month + '-' + day + 'T' + hour + ':' + minute + ':00');
        }
    });
}

function initSearchableSelects(root, attempt) {
    attempt = attempt || 0;
    if (typeof $.fn.select2 === 'undefined') {
        // CDN may lag behind site.js; retry briefly instead of leaving native selects visible.
        if (attempt < 40) {
            setTimeout(function () { initSearchableSelects(root, attempt + 1); }, 50);
        }
        return;
    }

    var container = root || document;
    $(container).find('.searchable-select').each(function () {
        var $select = $(this);
        if ($select.hasClass('select2-hidden-accessible')) {
            return;
        }

        var $modal = $select.closest('#itemDetailsModal');
        var options = {
            placeholder: function () {
                return $select.data('placeholder') || '';
            },
            allowClear: true,
            width: '100%'
        };

        if ($modal.length) {
            options.dropdownParent = $modal;
        }

        $select.select2(options);
    });
}

/* Boot combos as soon as DOM is ready — do not wait for other init work. */
(function bootSearchableSelectsEarly() {
    function run() {
        if (typeof window.jQuery === 'undefined') {
            return;
        }
        initSearchableSelects(document);
    }
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', run);
    } else {
        run();
    }
})();

function initDatePickers(root) {
    if (typeof flatpickr === 'undefined') {
        return;
    }

    var container = root || document;
    var pickers = container.querySelectorAll('.flatpickr-datetime');
    pickers.forEach(function (el) {
        if (el._flatpickr) {
            return;
        }

        var input = el.querySelector('[data-input]');
        var useNowForInsert = isPortalInsertContext(el);
        if (input && isSentinelDateValue(input.value)) {
            input.value = useNowForInsert ? formatPortalDateTime(new Date()) : '';
        }

        var options = {
            enableTime: true,
            enableSeconds: false,
            dateFormat: 'd.m.Y H:i',
            allowInput: true,
            time_24hr: true,
            wrap: true,
            disableMobile: true,
            onReady: function (_selectedDates, _dateStr, instance) {
                if (instance.input && isSentinelDateValue(instance.input.value)) {
                    if (useNowForInsert) {
                        instance.setDate(formatPortalDateTime(new Date()), false);
                    } else {
                        instance.clear();
                    }
                }

                // enableTime disables flatpickr's closeOnSelect — close when a calendar day is clicked.
                if (instance.daysContainer && !instance.daysContainer._portalDayCloseBound) {
                    instance.daysContainer._portalDayCloseBound = true;
                    instance.daysContainer.addEventListener('click', function (e) {
                        var day = e.target && e.target.closest
                            ? e.target.closest('.flatpickr-day')
                            : null;
                        if (!day || day.classList.contains('flatpickr-disabled')) {
                            return;
                        }
                        window.setTimeout(function () {
                            if (instance.isOpen) {
                                instance.close();
                            }
                        }, 0);
                    });
                }
            }
        };

        var htmlLang = (document.documentElement.lang || '').toLowerCase();
        if (htmlLang.startsWith('tr') && flatpickr.l10ns && flatpickr.l10ns.tr) {
            options.locale = flatpickr.l10ns.tr;
        }

        flatpickr(el, options);
    });
}

function loadJsonAllEditors() {
    let jsonEditors = $('.jsoneditor-class');
    jsonEditors.each(function (index) {
        const itemName = jsonEditors[index].id;
        const jsonReadonlyPrefix = "jsonEditorRO_";
        const isReadonly = itemName.startsWith(jsonReadonlyPrefix);
        const jsonPrefix = isReadonly ? jsonReadonlyPrefix : jsonReadonlyPrefix.replace('RO_', '_');
        loadJsonEditor(itemName, itemName.replace(jsonPrefix, ''), isReadonly);
    });
}

function loadYamlAllEditors(root) {
    const scope = root ? $(root) : $(document);
    scope.find('.yamleditor-class').each(function () {
        const el = this;
        const itemName = el.id;
        if (!itemName || typeof ace === 'undefined')
            return;

        // Already mounted (e.g. list modal behind a detail popup) — don't wipe content.
        if (el.env && el.env.editor) {
            el.env.editor.resize(true);
            return;
        }

        // Capture newlines via textContent BEFORE Ace mounts.
        // Ace's own DOM extraction uses innerText and collapses line breaks.
        const initialValue = (el.textContent || '').replace(/\r\n/g, '\n');
        el.textContent = '';

        el.style.width = el.style.width || '100%';
        el.style.height = el.style.height || '300px';
        el.style.maxWidth = '100%';
        el.style.whiteSpace = 'pre-wrap';
        el.style.overflow = 'hidden';
        el.style.position = 'relative';
        el.style.display = 'block';

        try {
            const jsonReadonlyPrefix = "yamlEditorRO_";
            const isReadonly = itemName.startsWith(jsonReadonlyPrefix);
            let editor = ace.edit(el);
            editor.session.setMode("ace/mode/yaml");
            editor.setTheme("ace/theme/github");
            editor.setReadOnly(isReadonly);
            editor.setOptions({
                wrap: true,
                autoScrollEditorIntoView: true
            });
            editor.setValue(initialValue, -1);
            editor.resize(true);
        } catch (err) {
            // Fallback: keep readable YAML if Ace fails to mount.
            el.textContent = initialValue;
            el.style.overflow = 'auto';
            el.style.visibility = 'visible';
            console.error('YAML editor failed to initialize', err);
        }
    });
}

function loadUmlAllEditors() {
    let jsonEditors = $('.umleditor-class');
    jsonEditors.each(function (index) {
        const itemName = jsonEditors[index].id;
        const jsonReadonlyPrefix = "umlEditorRO_";
        const isReadonly = itemName.startsWith(jsonReadonlyPrefix);
        let editor = ace.edit(itemName);
        editor.session.setMode("ace/mode/markdown");
        editor.setTheme("ace/theme/github");
        editor.setReadOnly(isReadonly);
    });
}

function addYamlEditorsToFormData(formData) {
    const yamlEditorPrefix = 'yamlEditor_';
    const yamlEditors = document.querySelectorAll(`[id^='${yamlEditorPrefix}']`);

    yamlEditors.forEach(editor => {
        const editorId = editor.id.replace(yamlEditorPrefix, '').replace('_','.');
        const aceEditor = ace.edit(editor.id);
        const editorContent = aceEditor.getValue().trim();
        formData.append(editorId, editorContent);
    });

    prepareFormData(formData);
}

function setPage(pageId) {
    var form = document.getElementById("formList");
    var currentPage = document.getElementById("CurrentPage");
    if (!form || !currentPage) return;
    currentPage.value = pageId;
    if (document.getElementById("list-region") && typeof refreshListRegion === "function") {
        refreshListRegion();
        return;
    }
    form.submit();
}

function setPageSize(pageSize) {
    var form = document.getElementById("formList");
    if (!form) return;

    var size = parseInt(pageSize, 10);
    if (!size || size < 1) return;

    var pageSizeInput = document.getElementById("PageSize");
    if (!pageSizeInput) {
        pageSizeInput = document.createElement("input");
        pageSizeInput.type = "hidden";
        pageSizeInput.id = "PageSize";
        pageSizeInput.name = "PageSize";
        form.appendChild(pageSizeInput);
    }

    var currentPage = document.getElementById("CurrentPage");
    pageSizeInput.value = String(size);
    if (currentPage) currentPage.value = "1";
    if (document.getElementById("list-region") && typeof refreshListRegion === "function") {
        refreshListRegion();
        return;
    }
    form.submit();
}

function needsBase64Encoding(value) {
    if (typeof value !== "string") return false;
    if (/^\s*[\{\[]/.test(value)) return true;
    if (/[\u0000-\u001F\u007F-\u009F<>"{}\[\]]/.test(value)) return true;
    if( /<[a-zA-Z][\s\S]*?>/.test(value)) return  true;
    return (value.length > 10000);
}

function base64Encode(str) {
    return btoa(encodeURIComponent(str).replace(/%([0-9A-F]{2})/g, (_, p1) =>
        String.fromCharCode("0x" + p1)
    ));
}

function base64Decode(str) {
    return decodeURIComponent(
        Array.prototype.map.call(atob(str), c =>
            '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
        ).join('')
    );
}

function prepareFormData(formData) {
    normalizePortalDateTimeFormData(formData);
    for (const key of formData.keys()) {
        let value = formData.get(key);
        if (needsBase64Encoding(value)) {
            formData.set(key, base64Encode(value) + "_IsBase64");
        }
    }
}

function loadJsonEditor(jsonEditorName, jsonDataItem, isReadonly) {
    const container = document.getElementById(jsonEditorName);
    let modes = ['code', 'text', 'tree'];
    const options = {
        mainMenuBar: true,
        navigationBar: true,
        statusBar: true,
        mode: 'code',
        modes: modes,
        onEditable: function (path, field, value) {
            return !isReadonly;
        },
        onChangeText: function (jsonString) {
            $('#' + jsonDataItem).val(jsonString);
        }
    }

    setJsonDataToEditor(container, options, jsonDataItem);
}

function setJsonDataToEditor(container, options, jsonDataItem) {
    const editor = new JSONEditor(container, options);
    let jsonValue = $('#' + jsonDataItem).val();
    let emptyJson = "{}";

    if (jsonValue.length === 0) {
        jsonValue = emptyJson;
    }
    try {
        const initialJson = JSON.parse(jsonValue);
        editor.set(initialJson);
    } catch {
        const initialJson = JSON.parse(emptyJson);
        editor.set(initialJson);
    }
}



$.validator.methods.range = function (value, element, param) {
    let globalizedValue = value.replace(",", ".");
    return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
}

$.validator.methods.number = function (value, element) {
    return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
}

