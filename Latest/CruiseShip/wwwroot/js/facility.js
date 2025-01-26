var dataTable;
$(document).ready(function () {
   
    loadDataTable();

});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: "/Admin/Facility/GetAll"},
        columns: [
            { data: 'name',"width":"15%" },
            { data: 'description', "width": "25%" },
            { data: 'fee', "width": "5%" },
            { data: 'availableSlots', "width": "15%" },
            { data: 'createdByUser.name', "width": "10%" },
            {
                data: 'imageURL',
                render: function (data) {
                    return `<img class="rounded-3" src="${data}" alt="Facility Image" style="max-width: 100px; max-height: 100px;" />`;
                },
                "width": "10%"
            },
            {
                data: 'id',
                render: function (data) {
                    let apiBaseUrl = `${window.location.origin}/api/Facilities`;
                    return `<div>
                                <a href="facility/upsert?id=${data}" class="btn btn-dark mx-2">Edit <i class="bi bi-pencil-square"></i> </a>
                                <a onClick=Delete('${apiBaseUrl}/${data}') class="btn btn-danger mx-2">Delete <i class="bi bi-trash3-fill"></i> </a>
                            </div>`;
                },
                "width": "20%"
            }
            
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}
