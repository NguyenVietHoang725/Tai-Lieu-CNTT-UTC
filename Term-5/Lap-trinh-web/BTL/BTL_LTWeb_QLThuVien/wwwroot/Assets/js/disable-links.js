

document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.disabled').forEach(function (element) {
        element.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation(); // Chặn luôn sự kiện lan ra ngoài
        });
    });
});

