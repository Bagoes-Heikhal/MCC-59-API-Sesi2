var exported = [0, 1, 2, 3, 4, 5, 6]

$.ajax({
    "url": "/Employees/getall",
    success: function (result) {
        console.log(result)
    },
    error: function (error) {
        console.log(error)
    }
})

$(document).ready(function () {
    table = $("#Employeetable").DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 20, -1], [5, 10, 20, 'All']],
        responsive: true,
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'copy',
                exportOptions: {
                    columns: exported,
                }
            },
            {
                extend: 'csv',
                exportOptions: {
                    columns: exported
                }
            },
            {
                extend: 'excel',
                title: 'List Employees',
                messageTop: 'Employee List of X Comppany.',
                exportOptions: {
                    columns: exported,
                    title: 'List Employees',
                }
            },
            {
                extend: 'pdf',
                title: 'List Employees',
                orientation: 'landscape',
                text: 'PDF',
                className: 'buttonHide',
                fileName: 'Data',
                autoFilter: true,
                messageTop: 'Employee List of X Comppany.',
                exportOptions: {
                    columns: exported
                }
            },
            {
                extend: 'print',
                title: 'List Employees',
                messageTop: 'Employee List of X Comppany.',
                exportOptions: {
                    columns: exported,
                }
            }
        ],
        "ajax": {
            "url": "/Employees/getall",
            dataSrc: ""
        },
        "columns": [

            {
                "data": null,
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "nik",
                dataSrc: ""
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `${row["firstName"]} ${row["lastName"]}`;
                }
            },
            { "data": "email" },
            {
                "data": null,
                "render": function (data, type, row) {
                    if (row['gender'] === 0) {
                        return `Male`;
                    } else {
                        return `Female`;
                    }
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["birthDate"]);
                }
            },
            { "data": "phone" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" 
                    onclick="getData('${row['nik']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                    <i class="fas fa-info-circle"></i> 
                    </button>
                    <button type="button" class="btn btn-danger" data-toggle="modal" onclick="Delete('${row['nik']}')" data-placement="top" title="Delete">
                    <i class="fas fa-trash-alt"></i> 
                    </button>
                    <button type="button" class="btn btn-info" data-toggle="modal" 
                    onclick="getDataUpdate('${row['nik']}')" title="Edit" data-target="#UpdateModals">
                    <i class="fas fa-edit"></i>
                    </button>`;
                }
            }
        ],
    });
});

function getData(nik) {
    $.ajax({
        url: "https://localhost:44345/API/Employees/" + nik,
        success: function (result) {
            var data = result.result
            console.log(data.firstName)
            var text = ""
            text =
           `<tr>
                <td> Name </td>
                <td> : </td>
                <td> ${data.firstName} ${data.lastName}</td>
            </tr>
            <tr>
                <td> NIK </td>
                <td> : </td>
                <td>${nik}</td>
            </tr>
            <tr>
                <td> Gender </td>
                <td> : </td>
                <td>${data.gender}</td>
            </tr>`
            $(".data-employ").html(text);
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function dateConversion(dates) {
    var date = new Date(dates)
    var newDate = ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '/' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '/' + date.getFullYear()
    return newDate
}

function Insert() {
    var obj = new Object();
    obj.nik = $("#nik").val();
    obj.firstName = $("#firstName").val();
    obj.lastName = $("#lastName").val();
    obj.email = $("#email").val();
    obj.phone = $("#phone").val();
    obj.salary = $("#salary").val();
    obj.gender = $("#gender").val();
    obj.birthDate = $("#dateBirth").val();
    console.log(obj)

    $.ajax({
        url: "/Employees/Post",
        type: "Post",
        'data': obj,
        'dataType': 'json',
        success: function (result) {
            console.log(result)
            if (result == 200) {
                Swal.fire(
                    'Good job!',
                    'Data berhasil di Submit!',
                    'success'
                ),
                    setTimeout(function () { $('#CreateModal').modal('hide'); }, 2000),
                    table.ajax.reload(),
                    setTimeout(function () { $("#createAccount")[0].reset(); }, 4000)
            } else if (result == 400){
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Submit Gagal!'
                })
            }
        },
        error: function (error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Submit Gagal!'
            })
        }
    })
    return false;
}

function resetValidate() {
    $('#createAccount').removeClass("was-validated")
}

function Delete(nik) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/Employees/Delete/" + nik,
                type: "Delete",  
                success: function (result) {
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    )
                    table.ajax.reload()
                },
                error: function (error) {
                    alert("Delete Fail");
                }
            });
        }
    })
}

function Update() {
    var obj = new Object();
    obj.nik = $("#updatenik").val();
    obj.firstName = $("#updatefirstName").val();
    obj.lastName = $("#updatelastName").val();
    obj.email = $("#updateemail").val();
    obj.phone = $("#updatephone").val();
    obj.salary = $("#updatesalary").val();
    obj.gender = $("#updategender").val();
    obj.birthDate = $("#updatedateBirth").val();
    console.log(obj)
    $.ajax({
        //headers: {
        //    'Accept': 'application/json',
        //    'Content-Type': 'application/json'
        //},
        url: "/Employees/Put",
        type: "Put",
        'data': obj,
/*        'data': JSON.stringify(obj),*/
        'dataType': 'json',
        success: function (result) {
            Swal.fire(
                'Good job!',
                'Your data has been saved!',
                'success'
            ),
                
                table.ajax.reload()
        },
        error: function (error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Submit Fail!'
            })
        }
    })
}

function getDataUpdate(nik) {
    $.ajax({
        url: "/Employees/get/" + nik,
        success: function (result) {
            console.log(result)
            var data = result
            $("#updatenik").attr("value", data.nik)
            $("#updatefirstName").attr("value", data.firstName)
            $("#updatelastName").attr("value", data.lastName)
            $("#updateemail").attr("value", data.email)
            $("#updatephone").attr("value", data.phone)
            $("#updatesalary").attr("value", data.salary)
            $("#updatedateBirth").attr("value", data.birthDate)
            $("#updategender").attr("value", data.gender)
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function resetFrom() {
    $("#createAccount")[0].reset() 
}

function RegisterEmployee() {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.nik = $("#nik").val();
    obj.firstName = $("#firstName").val();
    obj.lastName = $("#lastName").val();
    obj.phone = $("#phone").val();
    obj.email = $("#email").val();
    obj.password = $("#password").val();
    obj.salary = $("#salary").val();
    obj.birthdate = $("#dateBirth").val();
    obj.gpa = $("#gpa").val();
    var gender = $('#gender input:radio:checked').val()
    obj.gender = gender;
    obj.degree = $("#degree").val();
    obj.universityId = $("#universityId").val();

    console.log(obj);
    $.ajax({
        type: "POST",
        url: "/Employees/PostRegister",
        dataType: 'json',
        data: obj
    }).done((result) => {
        console.log(result);
        if (result == 200) {
            Swal.fire({
                icon: 'success',
                title: 'Berhasil Disimpan',
                showConfirmButton: false,
                timer: 1500
            })
        } else if (result == 400) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Gagal Disimpan'
            })
        }
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Gagal Disimpan'
        })
    })
}

$("#logout-btn").click(function () {
    HttpContext.Session.removeItem("JWToken");
    HttpContext.Session.Clear();
});