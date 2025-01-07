$(document).ready(function () {
    console.log("the script is runnnin ig");
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
            { data: 'createdByUser.userName', "width": "10%" },
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
                    return `<div class="btn-group" role="group">
                                <a href="facility/upsert?id=${data}" class="btn btn-dark mx-2">Edit <i class="bi bi-pencil-square"></i> </a>
                                <a href="facility/delete?id=${data}" class="btn btn-danger mx-2">Delete <i class="bi bi-trash3-fill"></i> </a>
                            </div>`;
                },
                "width": "20%"
            }
            
        ]
    });
}

