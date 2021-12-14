//const animals = [
//    { name: 'fluffy', species: 'cat', class: { name: 'mamalia' } },
//    { name: 'Nemo', species: 'fish', class: { name: 'vertebrata' } },
//    { name: 'hely', species: 'cat', class: { name: 'mamalia' } },
//    { name: 'Dory', species: 'fish', class: { name: 'vertebrata' } },
//    { name: 'ursa', species: 'cat', class: { name: 'mamalia' } }
//]

//let onlyCat = []
//for (var i = 0; i < animals.length; i++) {
//    if (animals[i].species == "cat") {
//        onlyCat.push(animals[i])
//    }
//}
//console.log(onlyCat)


//for (var i = 0; i < animals.length; i++) {
//    if (animals[i].species == "fish") {
//        animals[i].class.name = "non-mamalia"
//    }
//}
//console.log(animals)

//let name = document.querySelector(".data-table .name")
//let species = document.querySelector(".data-table .species")
//let species_class = document.querySelector(".data-table .class")

//name.innerHTML = "Name : " + animals[1].name
//species.innerHTML = "Species : " + animals[1].species
//species_class.innerHTML = "Class : " + animals[1].class.name

//var text = ""
//$.ajax({
//    url: "https://pokeapi.co/api/v2/pokemon",
//    success: function (result) {
//        $.each(result.results, function (key, val) {
//            text += `<tr class ="color-table">
//            <td class="text-center pt-3">${(key + 1)}</td>
//            <td class= "pt-3">${val.name}</td>
//            <td class="text-center" >
            //    <button type="button" class="btn btn-primary" data-toggle="modal" data-url="${val.url}"
            //    onclick="getData('${val.url}')" data-target="#exampleModal">
            //    Pokemon
            //</button>
//            </td>
//            </tr>`;
//        }),
//            $("#listSW").html(text);
//    },
//    error: function (error) {
//        console.log(error)
//    }
//})

function getData(url) {
    $.ajax({
        url: url,
        success: function (result) {
            $(".poke-pic").attr("src", result.sprites.other.dream_world.front_default)
            $(".modal-title").html(result.name)
            var text = ""
            text =
            `<tr class="poke" >
                <td> Id </td>
                <td> :  </td>
                <td>${result.id}</td>
            </tr>
            <tr class="poke ">
                <td> Name </td>
                <td> :  </td>
                <td>${result.name}</td>
            </tr>
            <tr class="poke" >
                <td> Xp </td>
                <td> :  </td>
                <td>${result.base_experience}</td>
            </tr>`
            $(".poke-ident").html(text);

            var types = ""
            $.each(result.types, function (key, val) {
                if (val.type.name == "grass") {
                    types += `<span class="Grass-bad badge badge-pill badge-success"> Grass </span>`
                } else if (val.type.name == "poison") {
                    types += `<span class="poison-bad badge badge-pill"> Poison </span>`
                } else if (val.type.name == "fire") {
                    types += `<span class="badge badge-pill badge-danger"> Fire </span>`
                } else if (val.type.name == "water") {
                    types += `<span class="badge badge-pill badge-info"> Water </span>`
                } else if (val.type.name == "bug") {
                    types += `<span class="bug-bad badge badge-pill badge-info"> Bug </span>`
                } else if (val.type.name == "flying") {
                    types += `<span class="bad badge badge-pill badge-secondary"> Flying </span>`
                } else if (val.type.name == "normal") {
                    types += `<span class="bad badge badge-pill badge-info badge-light"> Normal </span>`
                }
            });
            $(".poke-types").html(types);
        },
        error: function (error) {
            console.log(error)
        }
    })
}

$(".dark-btn").click(function () {
    $(".dark-btn i").toggleClass("far fa-moon")
    $(".dark-btn i").toggleClass("fas fa-moon")
    $(".dark-btn i").text(function (i, text) {
        return text === " Dark Mode" ? " Light Mode" : " Dark Mode"
    });
    $(".dark-btn").toggleClass("btn-outline-dark")
    $(".dark-btn").toggleClass("btn-dark")
    $(".head-table").toggleClass("table-success")
    $(".head-table").toggleClass("table-dark table-striped")
    $("#SWtable td").toggleClass("table-dark ")
});

$(document).ready(function () {
    var table = $("#SWtable").DataTable({
        "ajax": {
            "url": "https://pokeapi.co/api/v2/pokemon",
            "dataSrc": "results"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": 1 }
        ],
        "columns": [
            {
                "data": null
            },
            {
                "data": "name"
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" 
                    onclick="getData('${row["url"]}')" data-target="#exampleModal">
                    Pokemon
                    </button>`;
                }
            }
        ]
    });

    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

});