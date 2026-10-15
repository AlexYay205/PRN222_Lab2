// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const indexLink = document.getElementById('index-link');
let draggingIndex = false;

if (indexLink) {
    indexLink.addEventListener('dragstart', function (event) {
        draggingIndex = true;
        event.dataTransfer.setData('text/uri-list', indexLink.href);
        event.dataTransfer.effectAllowed = 'link';
    });

    // Only accept the Index link, leaving other drag operations unchanged.
    document.addEventListener('dragover', function (event) {
        if (draggingIndex) {
            event.preventDefault();
            event.dataTransfer.dropEffect = 'link';
        }
    });

    document.addEventListener('drop', function (event) {
        if (draggingIndex) {
            event.preventDefault();
            draggingIndex = false;
            window.location.assign(indexLink.href);
        }
    });

    indexLink.addEventListener('dragend', function () {
        draggingIndex = false;
    });
}
