(function () {
  "use strict";

  function busyLabelFor(original) {
    if (/sign in/i.test(original)) return "Signing in…";
    if (/create/i.test(original)) return "Creating account…";
    if (/send/i.test(original)) return "Sending…";
    if (/update/i.test(original)) return "Updating…";
    return "Please wait…";
  }

  function initPasswordToggles(root) {
    root.querySelectorAll("[data-auth-password-toggle]").forEach(function (button) {
      button.addEventListener("click", function () {
        var targetId = button.getAttribute("aria-controls");
        var input = targetId ? document.getElementById(targetId) : null;
        if (!input) {
          var wrap = button.closest(".auth-password");
          input = wrap ? wrap.querySelector("input") : null;
        }
        if (!input) return;

        var show = input.type === "password";
        input.type = show ? "text" : "password";
        button.setAttribute("aria-pressed", show ? "true" : "false");
        button.setAttribute("aria-label", show ? "Hide password" : "Show password");

        var icon = button.querySelector("i");
        if (icon) {
          icon.classList.toggle("fa-eye", !show);
          icon.classList.toggle("fa-eye-slash", show);
        }
      });
    });
  }

  function initSubmitBusy(root) {
    root.querySelectorAll("form.auth-form").forEach(function (form) {
      form.addEventListener("submit", function () {
        if (window.jQuery) {
          var $form = window.jQuery(form);
          if ($form.data("validator") && !$form.valid()) {
            return;
          }
        }

        var btn = form.querySelector("button.auth-btn[type='submit'], button[type='submit'].auth-btn, button.auth-btn");
        if (!btn || btn.classList.contains("is-busy")) {
          return;
        }

        var original = (btn.textContent || "").trim() || "Submit";
        btn.classList.add("is-busy");
        btn.setAttribute("aria-busy", "true");
        btn.innerHTML =
          '<span class="auth-btn__spinner" aria-hidden="true"></span>' +
          "<span>" +
          busyLabelFor(original) +
          "</span>";

        // Disable after the browser has queued the submit (disabled submit can cancel POST).
        window.setTimeout(function () {
          btn.disabled = true;
        }, 0);
      });
    });
  }

  document.addEventListener("DOMContentLoaded", function () {
    initPasswordToggles(document);
    initSubmitBusy(document);
  });
})();
