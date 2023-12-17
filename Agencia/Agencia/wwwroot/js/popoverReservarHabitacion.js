document.addEventListener("DOMContentLoaded", function () {
    const popover_btn = document.getElementById("popover-btn");
    const popover = document.getElementById("popover");

    const number = document.querySelector(".room-index");

    const plus = document.querySelector(".plus");
    const minus = document.querySelector(".minus");
    const num = document.querySelector(".num");
    const plus_minors = document.querySelector(".plus-minors");
    const minus_minors = document.querySelector(".minus-minors");
    const num_minors = document.querySelector(".num-minors");

    const addRoomButton = document.getElementById("addRoom");
    const roomsContainer = document.getElementById("rooms");
    const applyButton = document.getElementById("btn-apply");
    const applyDiv = document.getElementById("apply-four-rooms");

    const room_amount = document.querySelector(".room-amount");
    const people_amount = document.querySelector(".total-people");

    let roomCount = 1;

    const roomId = `room-${roomCount}`;
    const adultsCounts = {};
    const minorsCounts = {};
    const total_people_rooms = {};

    adultsCounts[roomId] = 1;
    minorsCounts[roomId] = 0;
    total_people_rooms[roomId] = 1;
    total_people = 1;

    let isPopoverOpen = true;

    popover_btn.addEventListener("click", function (event) {
        event.preventDefault();
        if (isPopoverOpen) {
            popover.style.opacity = 1;
            popover.style.visibility = "visible";
        } else {
            popover.style.opacity = 0;
            popover.style.visibility = "hidden";
            isPopoverOpen = !isPopoverOpen;
        }
    });

    plus.addEventListener("click", () => {
        if (adultsCounts[roomId] < 8 && total_people < 8) {
            adultsCounts[roomId]++;
            total_people_rooms[roomId]++;
            total_people++;
            num.innerText = adultsCounts[roomId];
            if (total_people >= 8) {
                addRoomButton.style.display = "none";
                applyDiv.classList.add("justify-content-end");
                applyDiv.classList.remove("justify-content-between");
            }
        }
    });

    minus.addEventListener("click", () => {
        if (adultsCounts[roomId] > 1) {
            adultsCounts[roomId]--;
            total_people_rooms[roomId]--;
            total_people--;
            num.innerText = adultsCounts[roomId];
            if (total_people < 8 && roomCount < 4) {
                addRoomButton.style.display = "block";
                applyDiv.classList.remove("justify-content-end");
                applyDiv.classList.add("justify-content-between");
            }
        }
    });

    plus_minors.addEventListener("click", () => {
        if (minorsCounts[roomId] < 7 && total_people < 8) {
            minorsCounts[roomId]++;
            total_people_rooms[roomId]++;
            total_people++;
            num_minors.innerText = minorsCounts[roomId];
            if (total_people >= 8) {
                addRoomButton.style.display = "none";
                applyDiv.classList.add("justify-content-end");
                applyDiv.classList.remove("justify-content-between");
            }
        }
    });

    minus_minors.addEventListener("click", () => {
        if (minorsCounts[roomId] > 0) {
            minorsCounts[roomId]--;
            total_people_rooms[roomId]--;
            total_people--;
            num_minors.innerText = minorsCounts[roomId];
            if (total_people < 8 && roomCount < 4) {
                addRoomButton.style.display = "block";
                applyDiv.classList.remove("justify-content-end");
                applyDiv.classList.add("justify-content-between");
            }
        }
    });

    addRoomButton.addEventListener("click", () => {
        if (roomCount < 4) {
            roomCount++;
            total_people++;
            number.innerText = roomCount;
            const roomId = `room-${roomCount}`;
            const roomDiv = document.createElement("div");
            roomDiv.classList.add("room");
            roomDiv.id = roomId;
            roomDiv.innerHTML = `
                            <div class="d-flex justify-content-end align-items-center border-top pt-2">
                                <button class="remove-room btn btn-danger btn-sm fs-6 me-3 rounded-pill">Eliminar</button>
                            </div>
                            <div class="d-flex justify-content-between px-3 pt-1 pb-2">
                                <div class="d-block">
                                    <p class="fs-5 text-body m-0">Adultos</p>
                                    <p class="popover-sml-txt text-body text-opacity-50 m-0">Desde 18 años</p>
                                </div>
                                <div class="wrapper mt-2">
                                    <span class="minus text-body">-</span>
                                    <span class="num text-body">1</span>
                                    <span class="plus text-body">+</span>
                                </div>
                            </div>
                            <div class="d-flex justify-content-between px-3 py-2">
                                <div class="d-block">
                                    <p class="fs-5 text-body m-0">Menores</p>
                                    <p class="popover-sml-txt text-body text-opacity-50 m-0">Hasta 17 años</p>
                                </div>
                                <div class="wrapper mt-2">
                                    <span class="minus-minors text-body">-</span>
                                    <span class="num-minors text-body">0</span>
                                    <span class="plus-minors text-body">+</span>
                                </div>
                            </div>
                        `;
            roomsContainer.appendChild(roomDiv);

            const plus = roomDiv.querySelector(".plus");
            const minus = roomDiv.querySelector(".minus");
            const num = roomDiv.querySelector(".num");
            const plus_minors = roomDiv.querySelector(".plus-minors");
            const minus_minors = roomDiv.querySelector(".minus-minors");
            const num_minors = roomDiv.querySelector(".num-minors");

            const removeButton = roomDiv.querySelector(".remove-room");

            adultsCounts[roomId] = 1;
            minorsCounts[roomId] = 0;
            total_people_rooms[roomId] = 1;

            if (roomCount > 2) {
                const lastRoom = document.getElementById(
                    `room-${roomCount - 1}`
                );
                const lastRoomRemoveButton =
                    lastRoom.querySelector(".remove-room");
                lastRoomRemoveButton.style.opacity = 0;
                lastRoomRemoveButton.style.visibility = "hidden";
            }

            plus.addEventListener("click", () => {
                if (adultsCounts[roomId] < 8 && total_people < 8) {
                    adultsCounts[roomId]++;
                    total_people_rooms[roomId]++;
                    total_people++;
                    num.innerText = adultsCounts[roomId];
                    if (total_people >= 8) {
                        addRoomButton.style.display = "none";
                        applyDiv.classList.add("justify-content-end");
                        applyDiv.classList.remove("justify-content-between");
                    }
                }
            });

            minus.addEventListener("click", () => {
                if (adultsCounts[roomId] > 1) {
                    adultsCounts[roomId]--;
                    total_people_rooms[roomId]--;
                    total_people--;
                    num.innerText = adultsCounts[roomId];
                    if (total_people < 8 && roomCount < 4) {
                        addRoomButton.style.display = "block";
                        applyDiv.classList.remove("justify-content-end");
                        applyDiv.classList.add("justify-content-between");
                    }
                }
            });

            plus_minors.addEventListener("click", () => {
                if (minorsCounts[roomId] < 7 && total_people < 8) {
                    minorsCounts[roomId]++;
                    total_people_rooms[roomId]++;
                    total_people++;
                    num_minors.innerText = minorsCounts[roomId];
                    if (total_people >= 8) {
                        addRoomButton.style.display = "none";
                        applyDiv.classList.add("justify-content-end");
                        applyDiv.classList.remove("justify-content-between");
                    }
                }
            });

            minus_minors.addEventListener("click", () => {
                if (minorsCounts[roomId] > 0) {
                    minorsCounts[roomId]--;
                    total_people_rooms[roomId]--;
                    total_people--;
                    num_minors.innerText = minorsCounts[roomId];
                    if (total_people < 8 && roomCount < 4) {
                        addRoomButton.style.display = "block";
                        applyDiv.classList.remove("justify-content-end");
                        applyDiv.classList.add("justify-content-between");
                    }
                }
            });

            removeButton.addEventListener("click", (event) => {
                event.stopPropagation();
                roomsContainer.removeChild(roomDiv);
                roomCount--;
                number.innerText = roomCount;
                total_people -= total_people_rooms[roomId];
                delete total_people_rooms[roomId];

                if (roomCount > 1) {
                    const lastRoom = document.getElementById(
                        `room-${roomCount}`
                    );
                    const lastRoomRemoveButton =
                        lastRoom.querySelector(".remove-room");
                    lastRoomRemoveButton.style.opacity = 1;
                    lastRoomRemoveButton.style.visibility = "visible";
                }

                if (roomCount < 4) {
                    addRoomButton.style.display = "block";
                    applyDiv.classList.remove("justify-content-end");
                    applyDiv.classList.add("justify-content-between");
                }
            });
        }
        if (roomCount >= 4) {
            addRoomButton.style.display = "none";
            applyDiv.classList.add("justify-content-end");
            applyDiv.classList.remove("justify-content-between");
        }
    });

    document.addEventListener("click", function (event) {
        if (
            !popover.contains(event.target) &&
            event.target !== popover_btn &&
            !popover_btn.contains(event.target)
        ) {
            popover.style.opacity = 0;
            popover.style.visibility = "hidden";
            room_amount.value = roomCount;
            people_amount.value = total_people;
            document.getElementById("total_people_rooms").value =
                JSON.stringify(total_people_rooms);
            //document.getElementById("total_adults").value =
            //    JSON.stringify(adultsCounts);
            //document.getElementById("total_minors").value =
            //    JSON.stringify(minorsCounts);
        }
    });

    applyButton.addEventListener("click", function () {
        isPopoverOpen = !isPopoverOpen;
        room_amount.value = roomCount;
        people_amount.value = total_people;
        document.getElementById("total_people_rooms").value =
            JSON.stringify(total_people_rooms);
        //document.getElementById("total_adults").value =
        //    JSON.stringify(adultsCounts);
        //document.getElementById("total_minors").value =
        //    JSON.stringify(minorsCounts);
    });
});
