// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*const currentLocation = location.href;
const menuItem = document.querySelectorAll('a');
const menuLength = menuItem.length;
for (let i = 0; i < menuLength; i++) {
    if (menuItem[i].href === currentLocation) {
        menuItem[i].className = "nav-link active";
    } else {
        menuItem[i].className = "nav-link";
    }
}
*/
//const myElement = document.getElementById("card1");
//myElement.className = "card text-white bg-danger mb-3";

//document.getElementById("card1").addEventListener("click", ubahWarna1);

/*function ubahWarna1() {
    const getClass = document.getElementById("card1");
    if (getClass.className == "card text-white bg-info mb-3") {
        document.getElementById("card1").className = "card text-white bg-warning mb-3";
    } else {
        document.getElementById("card1").className = "card text-white bg-info mb-3";
    }
}*/

//document.getElementById("btn2").addEventListener("paragraf", ubahParagraf);

/*function ubahParagraf() {
    const getPar = document.getElementById("paragraf");
    const getBtn = document.getElementById("btn2");
    if (getPar.className == "card-text text-justify") {
        document.getElementById("paragraf").className = "card-text text-left";
        getBtn.innerHTML = "Change to Rigth";

    } else if (getPar.className == "card-text text-left") {
        document.getElementById("paragraf").className = "card-text text-right";
        getBtn.innerHTML = "Change to Center";
    } else if (getPar.className == "card-text text-right") {
        document.getElementById("paragraf").className = "card-text text-center";
        getBtn.innerHTML = "Change to Justify";
    } else {
        document.getElementById("paragraf").className = "card-text text-justify";
        getBtn.innerHTML = "Change to Left";
    }
}*/

/*function ubahWarna() {
    const getClass = document.getElementById("card2");
    const getButton = document.getElementById("btn1");
    if (getButton.value == "red") {
        getButton.value = "yellow"
        getClass.className = "card text-white bg-warning mb-3";
        getButton.className = "btn btn-warning";
        getButton.innerHTML = "Change to Green";
    } else if (getButton.value == "yellow") {
        getButton.value = "green"
        getClass.className = "card text-white bg-success mb-3";
        getButton.className = "btn btn-success";
        getButton.innerHTML = "Change to Blue";
    } else if (getButton.value == "green") {
        getButton.value = "blue"
        getClass.className = "card text-white bg-primary mb-3";
        getButton.className = "btn btn-primary";
        getButton.innerHTML = "Change to Red";
    } else {
        getButton.value = "red"
        getClass.className = "card text-white bg-danger mb-3";
        getButton.className = "btn btn-danger";
        getButton.innerHTML = "Change to Yellow";
    }
}*/

//JQUERY
/*$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/",
}).done((result) => {
    console.log(result.results);
    let text = "";
    $.each(result.results, function (key, val) {
        console.log(val.name);
        text += `<tr>
                    <td class="col-1 text-center">${key + 1}</td>
                    <td>${val.name}</td>
                    <td class="col-2 text-center">
                        <button type="button" onclick="detailPoke('https://pokeapi.co/api/v2/pokemon/${val.name}')" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalDetail">
                                Detail
                        </button>
                    </td>
                </tr>`
    });
    //console.log(text);
    $("#tablePoke").html(text);
}).fail((error) => {
    console.log(error);
})*/

$(document).ready(function () {
    let table = $("#myTable").DataTable({
        ajax: {
            url: "https://pokeapi.co/api/v2/pokemon?limit=100000&offset=0",
            dataSrc: "results",
            dataType: "JSON"
        },
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { data: "name" },
            {
                data: "url",
                render: function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalDetail" onclick="detailPoke('${data}')">Detail</button>`;
                }
            },
            {
                render: function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#employeeModal" onclick="openAddEmpModal()">Employee</button>`;
                }
            },
            {
                render: function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#orderModal" onclick="orderPokeModal()">Order Pokemon</button>`;
                }
            }
        ]
    });
});

function detailPoke(url) {
    $.ajax({
        url
    }).done(e => {
        console.log(e);
        $("#imgPoke").attr("src", e.sprites.other.dream_world.front_default);
        $("#imgPoke2").attr("src", e.sprites.other.home.front_default);
        $("#imgPoke3").attr("src", e.sprites.other.home.front_shiny);
        $(".namePoke").text(e.name);
        $("#base_experience").width(e.base_experience);
        $("#height").text(e.height);
        $("#weight").text(e.weight);
        $("#order").text(e.order);
        $("#abil").text(e.abilities[0].ability.name);
        $("#hp").width(e.stats[0].base_stat + "%");
        $("#attack").width(e.stats[1].base_stat + "%");
        $("#defense").width(e.stats[2].base_stat + "%");
        $("#special-attack").width(e.stats[3].base_stat + "%");
        $("#special-defense").width(e.stats[4].base_stat + "%");
        $("#speed").width(e.stats[5].base_stat + "%");

        $("#statsRadarChart").remove();
        $(".radar-chart-container").append('<canvas id="statsRadarChart"></canvas>');
        // Create a radar chart for the Pokémon's stats
        createRadarChart([e.stats[0].base_stat, e.stats[1].base_stat, e.stats[2].base_stat, e.stats[3].base_stat, e.stats[4].base_stat, e.stats[5].base_stat]);

        texts = "";
        $.each(e.types, function (val) {
            if (e.types[val].type.name == "grass") {
                texts += `<span class="badge bg-success">${e.types[val].type.name}</span>`;
            } else if (e.types[val].type.name == "poison") {
                texts += `<span class="badge bg-danger ml-2">${e.types[val].type.name}</span>`;
            } else if (e.types[val].type.name == "fire") {
                texts += `<span class="badge bg-danger ml-2">${e.types[val].type.name}</span>`;
            } else if (e.types[val].type.name == "water") {
                texts += `<span class="badge bg-primary ml-2">${e.types[val].type.name}</span>`;
            } else {
                texts += `<span class="badge bg-warning ml-2">${e.types[val].type.name}</span>`;
            }
        });
        console.log(texts);
        $("#types").html(texts);
    })
}
//console.log(url);
//$("#imgPoke").html();

function createRadarChart(baseStat) {
    var ctx = document.getElementById('statsRadarChart').getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'radar',
        data: {
            labels: ['HP', 'Attack', 'Defense', 'Special Attack', 'Special Defense', 'Speed'],
            datasets: [{
                label: 'Base Stats',
                data: baseStat,
                backgroundColor: 'rgba(0, 123, 255, 0.5)',
                borderColor: 'rgba(0, 123, 255, 1)',
                borderWidth: 1,
            }]
        },
        options: {
            scale: {
                angleLines: {
                    display: false
                },
                ticks: {
                    suggestedMin: 0,
                    suggestedMax: 100,
                    stepSize: 20
                }
            }
        }
    });
}

// Example starter JavaScript for disabling form submissions if there are invalid fields
(function () {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }
              
                form.classList.add('was-validated')
            }, false)
        })
})()

function openAddEmpModal() {
    $("#employeeModal").modal("show");
}

function orderPokeModal() {
    $("#orderModal").modal("show");
}


//$(document).ready(function () {
//    $('#myTable').DataTable();
//});

//$(document).ready(function () {
//    $('#myTable').DataTable({
//        "ajax": {
//            "url": "https://pokeapi.co/api/v2/pokemon/",
//            "dataType": "json",
//            "dataSrc": "results"
//        },
//        "columns": [
//            {
//                "data": "name",
//            },
//            {
//                "data": "url",
//            },
//            {
//                "data": "url",
//            },
//        ]
//    })
//})

$(document).ready(function () {
    $('#Employee').DataTable({
        ajax: {
            url: "https://localhost:7043/api/Employee",
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
                    return `<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#employeeModal" onclick="openAddEmpModal()">Employee</button>`;
                }
            }
        ]
    });
});

function AddEmployee() {
    var eNik = $('#employee-nik').val();
    var eFirst = $('#employee-fname').val();
    var eLast = $('#employee-lname').val();
    var eBDate = $('#employee-bdate').val();
    var eGender = $('#employee-gender').val();
    var eHDate = $('#employee-hdate').val();
    var eEmail = $('#employee-email').val();
    var ePhone = $('#employee-pnumber').val();

    $.ajax({
        async: true, // Async by default is set to “true” load the script asynchronously  
        // URL to post data into sharepoint list  
        url: "https://localhost:7043/api/Employee",
        method: "POST", //Specifies the operation to create the list item  
        data: JSON.stringify({
            '__metadata': {
                'type': 'SP.Data.EmployeeListItem' // it defines the ListEnitityTypeName  
            },
            //Pass the parameters
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
            "accept": "application/json;odata=verbose", //It defines the Data format   
            "content-type": "application/json;odata=verbose", //It defines the content type as JSON  
/*            "X-RequestDigest": $("#__REQUESTDIGEST").val() //It gets the digest value   
*/        },
        success: function (data) {
            console.log(data);
        },
        error: function (error) {
            console.log(JSON.stringify(error));

        }

    })

}