document.addEventListener("DOMContentLoaded", function () {
    const popoverVuelo_btn = document.getElementById("popoverVuelo-btn");
    const popoverVuelo = document.getElementById("popoverVuelo");

    const plus = document.querySelector(".plus");
    const minus = document.querySelector(".minus");
    const num = document.querySelector(".num");
    const plus_minors = document.querySelector(".plus-minors");
    const minus_minors = document.querySelector(".minus-minors");
    const num_minors = document.querySelector(".num-minors");

    const applyButton = document.getElementById("btn-apply");

    const people_amount = document.querySelector(".total-people");

    adultsCounts = 1;
    minorsCounts = 0;
    total_people = 1;

    let isPopoverOpen = true;

    popoverVuelo_btn.addEventListener("click", function (event) {
        event.preventDefault();
        if (isPopoverOpen) {
            popoverVuelo.style.opacity = 1;
            popoverVuelo.style.visibility = "visible";
        } else {
            popoverVuelo.style.opacity = 0;
            popoverVuelo.style.visibility = "hidden";
            isPopoverOpen = !isPopoverOpen;
        }
    });

    plus.addEventListener("click", () => {
        if (adultsCounts < 8 && total_people < 8) {
            adultsCounts++;
            total_people++;
            num.innerText = adultsCounts;
        }
    });

    minus.addEventListener("click", () => {
        if (adultsCounts > 1) {
            adultsCounts--;
            total_people--;
            num.innerText = adultsCounts;
        }
    });

    plus_minors.addEventListener("click", () => {
        if (minorsCounts < 7 && total_people < 8) {
            minorsCounts++;
            total_people++;
            num_minors.innerText = minorsCounts;
        }
    });

    minus_minors.addEventListener("click", () => {
        if (minorsCounts > 0) {
            minorsCounts--;
            total_people--;
            num_minors.innerText = minorsCounts;
        }
    });

    document.addEventListener("click", function (event) {
        if (
            !popoverVuelo.contains(event.target) &&
            event.target !== popoverVuelo_btn &&
            !popoverVuelo_btn.contains(event.target)
        ) {
            popoverVuelo.style.opacity = 0;
            popoverVuelo.style.visibility = "hidden";
            people_amount.value = total_people;
            document.getElementById("total_adults").value =
                JSON.stringify(adultsCounts);
            document.getElementById("total_minors").value =
                JSON.stringify(minorsCounts);
        }
    });

    applyButton.addEventListener("click", function () {
        isPopoverOpen = !isPopoverOpen;
        people_amount.value = total_people;
        document.getElementById("total_adults").value =
            JSON.stringify(adultsCounts);
        document.getElementById("total_minors").value =
            JSON.stringify(minorsCounts);
    });
});
