// Get the menu button and the menu element
const mobilemenuBtn = document.getElementById('mobile-btn');
const mobilemenuMenu = document.getElementById('mobile-menu');

// Add a click event listener to the button
mobilemenuBtn.addEventListener('click', () => {
    // Toggle the visibility of the menu element
    mobilemenuMenu.classList.toggle('hidden');
});


const closeButton = document.getElementById('success-modal-close-btn');
const modal = document.querySelector('.fixed.z-10.inset-0.overflow-y-auto');

closeButton.addEventListener('click', () => {
    modal.classList.add('hidden');
});