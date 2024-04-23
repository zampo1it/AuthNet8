function blockUsers() {
    var checkboxes = document.getElementsByName("selectedUsers");
    var selectedUsers = [];
    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked) {
            var blockedCell = checkboxes[i].parentNode.nextElementSibling.nextElementSibling;
            blockedCell.textContent = "YES";
            selectedUsers.push(checkboxes[i].value);
        }
    }
    if (selectedUsers.length > 0) {
        var form = document.getElementById("blockUsersForm");
        var formData = new FormData(form);
        formData.append("selectedUsers", selectedUsers.join(","));
        fetch(form.action, {
            method: form.method,
            body: formData
        })
            .then(response => {
                if (response.ok) {
                    console.log("Success");
                } else {
                    console.log("error");
                }
            })
            .catch(error => {
                // Handle error
            });
    }
}

function unblockUsers() {
    var checkboxes = document.getElementsByName("selectedUsers");
    var selectedUsers = [];
    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked) {
            var blockedCell = checkboxes[i].parentNode.nextElementSibling.nextElementSibling;
            blockedCell.textContent = "NO";
            selectedUsers.push(checkboxes[i].value);
        }
    }
    if (selectedUsers.length > 0) {
        var form = document.getElementById("unblockUsersForm");
        var formData = new FormData(form);
        formData.append("selectedUsers", selectedUsers.join(","));
        fetch(form.action, {
            method: form.method,
            body: formData
        })
            .then(response => {
                if (response.ok) {
                    console.log("Success");
                } else {
                    console.log("error");
                }
            })
            .catch(error => {
                // Handle error
            });
    }
}

function deleteUsers() {
    var checkboxes = document.getElementsByName("selectedUsers");
    var selectedUsers = [];
    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked) {
            selectedUsers.push(checkboxes[i].value);
            var row = checkboxes[i].parentNode.parentNode;
            row.remove();
        }
    }
    if (selectedUsers.length > 0) {
        var form = document.getElementById("deleteUsersForm");
        var formData = new FormData(form);
        formData.append("selectedUsers", selectedUsers.join(","));

        fetch(form.action, {
            method: form.method,
            body: formData
        })
            .then(response => {
                if (response.ok) {
                    console.log("Success");
                } else {
                    console.log("error");
                }
            })
            .catch(error => {
                // Handle error
            });
    }
}

document.getElementById("selectAll").addEventListener("change", function () {
    var checkboxes = document.getElementsByName("selectedUsers");
    for (var i = 0; i < checkboxes.length; i++) {
        checkboxes[i].checked = this.checked;
    }
});