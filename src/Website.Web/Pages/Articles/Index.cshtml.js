

$(function () {
    /*var getFilter = function () {
        console.log("---start-----")
        return {
            filter: $("#Search").val(),
        };
    };*/
    var l = abp.localization.getResource('Website');
    var editModal = new abp.ModalManager(abp.appPath + 'Articles/EditArticleModal');
    
    var dataTable = $('#ArticlesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[0, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(
                website.articles.article.getList),
            columnDefs: [
                /* TODO: Column definitions */
                {
                    title: l('Name'),
                    data: "name",

                },
                {
                    title: l('CategoryName'),
                    data: "categoryName",
                    orderable: false
                },
                {
                    title: l('UserName'),
                    data: "identityUserUsername",
                    orderable: false
                },

                {
                    title: l('Status'),
                    data: "status",
                    render: function (data) {
                        return l('Enum:Status:' + data);
                    }
                },
                {
                    title: l('DatePublishment'),
                    data: "datePublishment",
                    dataFormat: 'date'
                },
                {
                    title: l('DateCreation'),
                    data: "dateCreation",
                    dataFormat: 'date'
                },



                {
                    title: l('ContentBody'),
                    data: "contentBody",

                },

                {
                    title: l('Teaser'),
                    data: "teaser",

                },

                {
                    title: l('Image'),
                    data: "image",
                    render: function (data) {
                        
                            /*
                            ./ Uploads / igmsvgw1.ezf.jpg
                            <img src="./Uploads/igmsvgw1.ezf.jpg">
                            
                            <image src="C:/Users/caoph/Desktop/.Net toturial/Website/src/Website.Web/wwwroot/Uploads/igmsvgw1.ezf.jpg" />
                            */
                        return `<img src="${data}" alt="${data}">`  ;
                        
                    }

                },
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },

                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l('ProductDeletionConfirmationMessage',
                                            data.record.name);
                                    },
                                    action: function (data) {
                                        website.articles.article
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    }
                                },
                                {
                                    text: l('View'),
                                    action: function (data) {
                                        window.location.href = "/Articles/ViewArticle?Id=" + data.record.id;

                                        var id = data.record.id;
                                    }
                                }
                                
                            ]
                    }
                },
            ]
        })
    );
    var createModal = new abp.ModalManager(abp.appPath + 'Articles/CreateArticleModal');
    createModal.onResult(function () {
        dataTable.ajax.reload();
    });
    $('#NewArticleButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });


    //Category Table

    var dataTable2 = $('#CategoryTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[0, "asc"]],
            searching: false,
            ajax: abp.libs.datatables.createAjax(website.categories.category.getList),
            columnDefs: [
                {
                    title: l('Name'),
                    data: "name"
                },

                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    action: function (data) {
                                        window.location.href = "/Categories/EditCategory?Id=" + data.record.id;
                                        
                                        var id =  data.record.id;
                                    }
                                },

                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l('ProductDeletionConfirmationMessage',
                                            data.record.name);
                                    },
                                    action: function (data) {
                                        console.log('data: ', data);
                                        website.categories.category
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable2.ajax.reload();
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                
            ]
        })
    );


    //Search
    
    

    function initializeDataTable(input) {
        

        if (input !== '') {
            var ajaxConfig = abp.libs.datatables.createAjax(website.articles.article.getListSearch, input);
             
         } else {
             var ajaxConfig = abp.libs.datatables.createAjax(website.articles.article.getList)
         }
        
        dataTable = $('#ArticlesTable').DataTable(
            abp.libs.datatables.normalizeConfiguration({
                serverSide: true,
                paging: true,
                order: [[0, "asc"]],
                searching: false,
                scrollX: true,
                ajax:  ajaxConfig,
                columnDefs: [
                    /* TODO: Column definitions */
                    {
                        title: l('Name'),
                        data: "name",

                    },
                    {
                        title: l('CategoryName'),
                        data: "categoryName",
                        orderable: false
                    },
                    {
                        title: l('UserName'),
                        data: "identityUserUsername",
                        orderable: false
                    },

                    {
                        title: l('Status'),
                        data: "status",
                        render: function (data) {
                            return l('Enum:Status:' + data);
                        }
                    },
                    {
                        title: l('DatePublishment'),
                        data: "datePublishment",
                        dataFormat: 'date'
                    },
                    {
                        title: l('DateCreation'),
                        data: "dateCreation",
                        dataFormat: 'date'
                    },



                    {
                        title: l('ContentBody'),
                        data: "contentBody",

                    },

                    {
                        title: l('Teaser'),
                        data: "teaser",

                    },

                    {
                        title: l('Image'),
                        data: "image",
                        render: function (data) {

                            /*
                            ./ Uploads / igmsvgw1.ezf.jpg
                            <img src="./Uploads/igmsvgw1.ezf.jpg">
                            
                            <image src="C:/Users/caoph/Desktop/.Net toturial/Website/src/Website.Web/wwwroot/Uploads/igmsvgw1.ezf.jpg" />
                            */
                            return `<img src="${data}" alt="${data}">`;

                        }

                    },
                    {
                        title: l('Actions'),
                        rowAction: {
                            items:
                                [
                                    {
                                        text: l('Edit'),
                                        action: function (data) {
                                            editModal.open({ id: data.record.id });
                                        }
                                    },

                                    {
                                        text: l('Delete'),
                                        confirmMessage: function (data) {
                                            return l('ProductDeletionConfirmationMessage',
                                                data.record.name);
                                        },
                                        action: function (data) {
                                            website.articles.article
                                                .delete(data.record.id)
                                                .then(function () {
                                                    abp.notify.info(l('SuccessfullyDeleted'));
                                                    dataTable.ajax.reload();
                                                });
                                        }
                                    },
                                    {
                                        text: l('View'),
                                        action: function (data) {
                                            window.location.href = "/Articles/ViewArticle?Id=" + data.record.id;

                                            var id = data.record.id;
                                        }
                                    }

                                ]
                        }
                    },
                ]
            })
        );
    }

    document.getElementById("test").onsubmit = (e) => {
        e.preventDefault();

        
        dataTable.destroy();
        
        console.log('----start------');
        var input = document.getElementById("Search").value;
        console.log('input: ', input);

        if (input !== '') {
            
            initializeDataTable(input);
        } else {
            
            initializeDataTable('' ); // Call with empty input to load all data
        }
        console.log('----end------');
    };
    
});