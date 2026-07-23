$(function () {
    init();

    $('.opButtonDetail').click(function (e) {
        let selectedKey = this.id.replace('DetailItem_', '');
        $('#SelectedKey').val(selectedKey);
        $("#formList").data('SelectedKey',selectedKey);
        let moduleName = $(this).data('module-name');
        let actionName = "DetailItem";
        openModalPopup(moduleName, actionName);
    });

    $('.opButtonInsert').click(function (e) {
        let actionName = "InsertItem";
        let moduleName = $(this).data('module-name');
        openModalPopup(moduleName, actionName);
    });

    $('.opButtonDelete').click(function (e) {
        let selectedKey = this.id.replace('DeleteItem_', '');
        $('#SelectedKey').val(selectedKey);
        $("#formList").data('SelectedKey',selectedKey);
        let moduleName = $(this).data('module-name');
        let actionName = "DeleteItem";
        openModalPopup(moduleName, actionName);
    });

    $('.opButtonUpdate').click(function (e) {
        let selectedKey = this.id.replace('UpdateItem_', '');
        $('#SelectedKey').val(selectedKey);
        $("#formList").data('SelectedKey',selectedKey);
        let moduleName = $(this).data('module-name');
        let actionName = "UpdateItem";
        openModalPopup(moduleName, actionName);
    });

    function openModalPopup(moduleName, actionName) {
        let popupUrl = `/${moduleName}/${actionName}`;

        $.ajax({
            type: "POST",
            url: popupUrl,
            processData: false,
            data: $("#formList").serialize(),
            success: function (data) {
                $('#itemDetailsContainer').html(data);
                $('#itemDetailsContainer .modal-content').addClass('portal-entity-modal');
                enhancePortalEntityForm(document.getElementById('itemDetailsContainer'));
                // Bootstrap 5 compatible modal show
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
                    if (!detailsRoot)
                        return;
                    $(detailsRoot).find('.yamleditor-class').each(function () {
                        if (this.env && this.env.editor)
                            this.env.editor.resize(true);
                    });
                }

                // Mount Ace immediately (container has fixed height) so the plain-text
                // flash never appears; resize once the modal animation finishes.
                initModalEditors();

                if (modalElement && typeof bootstrap !== 'undefined') {
                    var modal = bootstrap.Modal.getOrCreateInstance(modalElement);
                    $(modalElement).one('shown.bs.modal', resizeYamlEditors);
                    modal.show();
                } else {
                    // Fallback to jQuery if Bootstrap 5 not available
                    $('#itemDetailsModal').one('shown.bs.modal', resizeYamlEditors).modal('show');
                }
            },
            error: function (xhr, textStatus, error) {
                console.log(xhr.statusText);
                console.log(textStatus);
                console.log(error);
            },
        });
    }
});

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
    const placeholderElement = $('#itemDetailsContainer');
    initFlatpickrModalFix();
    initDatePickers(document);
    initSearchableSelects(document);

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

    $(document).on('click', '[data-toggle="lightbox"]', function (event) {
        event.preventDefault();
        let targetId = '';
        $(this).ekkoLightbox({
            onShown: function (lb) {
                $(targetId).addClass('lbackground');
            },
            onShow: function (lb) {
                targetId = '#' + lb.delegateTarget.id
                $(targetId).addClass('lbackground');
            },
            onHidden: function () {
                $(targetId).removeClass('lbackground');
                if ($('.modal:visible').length) {
                    $('body').addClass('modal-open');
                    $('#itemDetailsModal').focus();
                }

            }
        });
    });
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

function initSearchableSelects(root) {
    if (typeof $.fn.select2 === 'undefined') {
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
        if (input && isSentinelDateValue(input.value)) {
            input.value = '';
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
                    instance.clear();
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
    $('#CurrentPage').val(pageId);
    $('#formList').submit();
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

