$(document).ready(function () {
    $('#Employee').DataTable({
        ajax: {
            url: "https://localhost:7065/api/Employee",
            dataSrc: "data",
            dataType: "JSON"
        },
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { data: "guid" },
            { data: "nik" },
            { data: "firstName" },
            { data: "lastName" },
            { data: "birthDate" },
            { data: "gender" },
            { data: "hiringDate" },
            { data: "email" },
            { data: "phoneNumber" },
            {
                render: function () {
                    return `<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#employeeModal" onclick="openAddEmpModal()">Add</button>`;
                }
            },
            {
                render: function (data, type, row, meta) {
                    return `<button type="button" class="btn btn-danger" onclick="deleteEmployee('${row.guid}')">Delete</button>`;
                }
            },
            {
                render: function (data, type, row, meta) {
                    return `<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#updateEmployeeModal" onclick="openEditEmpModal('${row?.guid}', '${row?.nik}', '${row?.firstName}', '${row?.lastName}', '${row.birthDate}', '${row.gender}', '${row.hiringDate}', '${row.email}', '${row.phoneNumber}')">Update</button>`;
                }
            }
        ],

    });

});

function updateEmployee(guid) {
    console.log('this guid: ', guid)
    // Retrieve the updated values from the input fields
    var eNik = document.getElementById('u-employee-nik').value;
    var eFirst = document.getElementById('u-employee-fname').value;
    var eLast = document.getElementById('u-employee-lname').value;
    var eBDate = document.getElementById('u-employee-bdate').value;
    var eGender = document.querySelector('input[name="u-employee-gender"]:checked').id.includes('m') ? 1 : 0;
    var eHDate = document.getElementById('u-employee-hdate').value;
    var eEmail = document.getElementById('u-employee-email').value;
    var ePhone = document.getElementById('u-employee-pnumber').value;

    $.ajax({
        url: `https://localhost:7065/api/Employee`,
        method: "PUT",
        data: JSON.stringify({
            '__metadata': {
                'type': 'SP.Data.EmployeeListItem'
            },
            'guid': guid,
            'nik': eNik,
            'firstName': eFirst,
            'lastName': eLast,
            'birthDate': eBDate,
            'gender': eGender,
            'hiringDate': eHDate,
            'email': eEmail,
            'phoneNumber': ePhone
        }),
        headers: {
            "accept": "application/json;odata=verbose",
            "content-type": "application/json;odata=verbose",
            "X-HTTP-Method": "MERGE",
            "IF-MATCH": "*"
        },
        success: function (data) {
            console.log(data);
            window.location.reload()
            // Get the updated row data as an array
            var updatedRowData = [
                eNik,
                eFirst,
                eLast,
                eBDate,
                eGender === 1 ? 'Male' : 'Female',
                eHDate,
                eEmail,
                ePhone
                // Add other columns as needed
            ];

            // Loop through each row in the DataTable
            dataTable.rows().every(function (rowIdx, tableLoop, rowLoop) {
                var rowData = this.data();

                // Check if the GUID in the row matches the guid parameter
                if (rowData[0] === guid) {
                    // Update the row data
                    this.data(updatedRowData);

                    // Redraw the updated row
                    this.invalidate();

                    // Exit the loop
                    return false;
                }
            });

            // Close the modal
            $('#employeeUpdateModal').modal('hide');
        },
        error: function (error) {
            console.log("Error: " + JSON.stringify(error));
        }
    });
}

function openEditEmpModal(guid, nik, firstName, lastName, birthDate, gender, hiringDate, email, phoneNumber) {
    // Set the values of the input fields in the modal
    /*console.log('woi ini dari update: ', nik)*/
    document.getElementById('u-employee-nik').value = nik;
    document.getElementById('u-employee-fname').value = firstName;
    document.getElementById('u-employee-lname').value = lastName;
    document.getElementById('u-employee-bdate').value = birthDate;
    document.getElementById('u-employee-hdate').value = hiringDate;
    document.getElementById('u-employee-email').value = email;
    document.getElementById('u-employee-pnumber').value = phoneNumber;

    // Set the gender radio button based on the gender value
    if (gender === 1) {
        document.getElementById('u-employee-gender-m').checked = true;
    } else {
        document.getElementById('u-employee-gender-f').checked = true;
    }

    // Change the modal title
    document.getElementById('employeeUpdateModalTitle').innerText = 'Update Employee';

    // Show the modal
    $('#employeeUpdateModal').modal('show');

    // Add an event listener to the form submit button for updating the employee
    document.getElementById('employeeUpdateModalBody').addEventListener('submit', function (event) {
        event.preventDefault(); // Prevent form submission

        // Call the updateEmployee function with the GUID parameter
        updateEmployee(guid);
    });
}

function openAddEmpModal() {
    $("#employeeModal").modal("show");

}

function AddEmployee() {
    var eNik = $('#employee-nik').val();
    var eFirst = $('#employee-fname').val();
    var eLast = $('#employee-lname').val();
    var eBDate = $('#employee-bdate').val();
    var eGender = document.querySelector('input[name="employee-gender"]:checked').id.includes('m') ? 1 : 0;
    var eHDate = $('#employee-hdate').val();
    var eEmail = $('#employee-email').val();
    var ePhone = $('#employee-pnumber').val();

    $.ajax({
        async: true,
        url: "https://localhost:7065/api/Employee",
        method: "POST",
        data: JSON.stringify({
            '__metadata': {
                'type': 'SP.Data.EmployeeListItem'
            },
            'nik': eNik,
            'firstName': eFirst,
            'lastName': eLast,
            'birthDate': eBDate,
            'gender': eGender,
            'hiringDate': eHDate,
            'email': eEmail,
            'phoneNumber': ePhone
        }),
        headers: {
            "accept": "application/json;odata=verbose",
            "content-type": "application/json;odata=verbose",
        },
        success: function (data) {
            console.log(data);
        },
        error: function (error) {
            console.log(JSON.stringify(error));
        }
    });
}


function deleteEmployee(guid) {
    console.log(guid)
    if (confirm("Are you sure you want to delete this employee?")) {
        $.ajax({
            url: `https://localhost:7065/api/Employee/${guid}`,
            type: 'DELETE',
            success: function (result) {
                console.log(result);
                window.location.reload();
            }, error: function (xhr, status, error) {
                console.error('error occured: ', error)
            }
        });
    }
}


function validateForm() {
    // Add your form validation logic here
    return true;
}