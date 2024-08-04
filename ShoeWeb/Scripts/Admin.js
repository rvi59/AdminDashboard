$(document).ready(function () {
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');

    });


    $('#sidebarCollapse1').on('click', function () {
        $('#sidebar').toggleClass('active');
    });


    $('.enlarged-image').hide();
    $('.image-container img').hover(
        function () {
            $('.enlarged-image').show();
            $('.enlarged-image').fadeIn();
        },
        function () {
            $('.enlarged-image').fadeOut();
        }
    );


    function displayCurrentDate() {
        const currentDate = new Date();
        const dayName = currentDate.toLocaleDateString('en-US', { weekday: 'long' });
        const dayNumber = currentDate.getDate();
        const monthName = currentDate.toLocaleDateString('en-US', { month: 'long' });
        const year = currentDate.getFullYear();

        const formattedDate = `${dayName}, ${dayNumber} ${monthName} ${year}`;
        document.getElementById('mydate').textContent = formattedDate;
    }

    displayCurrentDate();


});


