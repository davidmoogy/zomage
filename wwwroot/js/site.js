document.addEventListener('DOMContentLoaded', function() {
    
    // Dropdown
    var dropdown = document.querySelector('.nav-dropdown');
    var toggle = document.querySelector('.dropdown-toggle');

    if (toggle && dropdown) {
        toggle.addEventListener('click', function(e) {
            e.preventDefault();
            dropdown.classList.toggle('active');
        });

        document.addEventListener('click', function(e) {
            if (!dropdown.contains(e.target)) {
                dropdown.classList.remove('active');
            }
        });
    }

    // Search Bar
    var searchBtn = document.getElementById('search-btn');
    var searchBar = document.getElementById('search-bar');
    var closeSearch = document.getElementById('close-search');
    var searchInput = searchBar.querySelector('input');

    // Open search
    searchBtn.addEventListener('click', function() {
        searchBar.classList.add('active');
        searchInput.focus();
    });

    // Close search
    closeSearch.addEventListener('click', function() {
        searchBar.classList.remove('active');
    });

    // Close with ESC key
    document.addEventListener('keydown', function(e) {
        if (e.key === 'Escape') {
            searchBar.classList.remove('active');
        }
    });

    // Language Switch
    var langBtn = document.getElementById('lang-btn');
    var langFlag = document.getElementById('lang-flag');
    var currentLang = 'ka';

    langBtn.addEventListener('click', function() {
        if (currentLang === 'ka') {
            currentLang = 'en';
            langFlag.src = '/images/en.svg';
        } else {
            currentLang = 'ka';
            langFlag.src = '/images/ge.svg';
        }

        // Translate all text
        var elements = document.querySelectorAll('[data-ka][data-en]');
        elements.forEach(function(el) {
            if (el.tagName === 'INPUT') {
                el.placeholder = el.getAttribute('data-' + currentLang);
            } else {
                el.textContent = el.getAttribute('data-' + currentLang);
            }
        });
    });

    // Quantity Selector
    var quantitySelectors = document.querySelectorAll('.quantity-selector');
    
    quantitySelectors.forEach(function(selector) {
        var quantityValue = selector.querySelector('.quantity-value');
        var dropdown = selector.querySelector('.quantity-dropdown');
        var options = dropdown.querySelectorAll('.quantity-option');
        
        // Toggle dropdown
        selector.addEventListener('click', function(e) {
            e.stopPropagation();
            selector.classList.toggle('active');
        });
        
        // Select quantity option
        options.forEach(function(option) {
            option.addEventListener('click', function(e) {
                e.stopPropagation();
                var value = option.getAttribute('data-value');
                quantityValue.textContent = value;
                selector.classList.remove('active');
            });
        });
    });
    
    // Close dropdown when clicking outside
    document.addEventListener('click', function(e) {
        quantitySelectors.forEach(function(selector) {
            if (!selector.contains(e.target)) {
                selector.classList.remove('active');
            }
        });
    });

    // Video Modal
    var videoModal = document.getElementById('video-modal');
    var videoModalClose = document.getElementById('video-modal-close');
    var videoModalTitle = document.getElementById('video-modal-title');
    var videoIframe = document.getElementById('video-iframe');
    var videoModalOverlay = document.querySelector('.video-modal-overlay');
    var videoButtons = document.querySelectorAll('.product-video-btn');

    // Default YouTube video ID (you can change this or add video IDs per product)
    var defaultVideoId = 'aaaa'; // Replace with actual video IDs

    // Open video modal
    videoButtons.forEach(function(btn) {
        btn.addEventListener('click', function(e) {
            e.stopPropagation();
            var productName = btn.getAttribute('data-product-name');
            var productId = btn.getAttribute('data-product-id');
            
            // Set product title
            videoModalTitle.textContent = productName;
            
            // Set video URL (using default for now, you can customize per product)
            // Format: https://www.youtube.com/embed/VIDEO_ID
            videoIframe.src = 'https://www.youtube.com/embed/' + defaultVideoId + '?autoplay=1';
            
            // Show modal
            videoModal.classList.add('active');
            document.body.style.overflow = 'hidden';
        });
    });

    // Close video modal
    function closeVideoModal() {
        videoModal.classList.remove('active');
        videoIframe.src = ''; // Stop video playback
        document.body.style.overflow = '';
    }

    videoModalClose.addEventListener('click', closeVideoModal);
    videoModalOverlay.addEventListener('click', closeVideoModal);

    // Close modal with ESC key
    document.addEventListener('keydown', function(e) {
        if (e.key === 'Escape' && videoModal.classList.contains('active')) {
            closeVideoModal();
        }
    });

    // Mobile Menu
    var mobileMenuBtn = document.getElementById('mobile-menu-btn');
    var mobileMenu = document.getElementById('mobile-menu');
    var mobileNavDropdown = document.querySelector('.mobile-nav-dropdown');
    var mobileDropdownToggle = document.querySelector('.mobile-dropdown-toggle');

    if (mobileMenuBtn && mobileMenu) {
        mobileMenuBtn.addEventListener('click', function(e) {
            e.stopPropagation();
            mobileMenu.classList.toggle('active');
        });

        // Close mobile menu when clicking outside
        document.addEventListener('click', function(e) {
            if (!mobileMenu.contains(e.target) && !mobileMenuBtn.contains(e.target)) {
                mobileMenu.classList.remove('active');
            }
        });
    }

    // Mobile dropdown menu
    if (mobileDropdownToggle && mobileNavDropdown) {
        mobileDropdownToggle.addEventListener('click', function(e) {
            e.preventDefault();
            e.stopPropagation();
            mobileNavDropdown.classList.toggle('active');
        });
    }
});
