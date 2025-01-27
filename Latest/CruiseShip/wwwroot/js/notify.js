function updateBookingRequestNotificationCount() {

    $.ajax({
        url: "https://localhost:7061/api/Bookings/count/pending", // API URL for pending count
        type: "GET",
        success: function (response) {
            console.log(response)
            // Assuming your API returns { success: true, count: X }
            if (response.success) {
                const count = response.count;

                // Update the badge
                const badge = $(".icon-button__badge");
                badge.text(count);

                // Show or hide the badge based on count
                if (count === 0) {
                    badge.addClass("visually-hidden");
                } else {
                    badge.removeClass("visually-hidden");
                }
            }
        },
        error: function () {
            console.error("Failed to fetch book request notification count.");
        }
    });
}

// Update the badge every 60 seconds
setInterval(updateBookingRequestNotificationCount, 10000);

// Initial call to load the count
updateBookingRequestNotificationCount();
