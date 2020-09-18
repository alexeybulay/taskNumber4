// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function searchTag() {

    console.log("hello");
}

paginationTransition = function(form, pageNumber){
    $.ajax({
        type: 'GET',
        url: form.action,
        data: { page: pageNumber },
        success: function(response) {
            $.get(form.action,
                function (data) {
                    $("body").html(data);
                });
        },
        error: function (err) {
            console.log(err);
        }
    });
}

uploadImage = function(form, title, text,type)  {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (response) {
                document.getElementById("img").src = "/images/" + response.name;
                sweetAlert(title, text, type,
                    {
                        button: 'Хорошо'
                    });
                console.log(response.name);
            },
            error: function (err) {
                console.log(err);
            }
        });
        return false;
    } catch (ex) {
        throw ex;
    }
}

$(function () {
    $("#loaderbody").addClass('hide');

    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
});

showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function(response) {
            $("#exampleModal .modal-body").html(response);
            $("#exampleModal .modal-title").html(title);
            $("#exampleModal").modal('show');
        },
        error: function(err) {
            console.log(err);
        }
        });
}

$(function() {
    $('[data-toggle="tooltip"]').tooltip();
});
function closePop() {
    $("#exampleModal .modal-body").html('');
    $("#exampleModal .modal-title").html('');
    $("#exampleModal").modal('hide');
}
closePopUp = form => {
    try {
        $.ajax({
            type: 'GET',
            url: form.action,
            success: function (response) {
                $.get(null,
                    function(data) {
                        $("body").html(data);
                    });
                $("#exampleModal .modal-body").html('');
                $("#exampleModal .modal-title").html('');
                $("#exampleModal").modal('hide');
            },
            error: function(error) {
                console.log(error);
            }
        });
        return false;
    } catch (ex) {
        throw ex;
    }
}
function callSwallAlert(title, text, result) {

    swal(title, text, result);
}
    function callConfirmEmail() {
        swal({
                title: "Подтверждение почты",
                text: "Для отправки ссылки с активацией укажите вашу почту",
                type: "input",
                showCancelButton: true,
                closeOnConfirm: false,
                animation: "slide-from-top",
                inputPlaceholder: "Укажите почту"
            },
            function (emailAdress) {
                try {
                    $.ajax({
                        type: 'POST',
                        url: "/Home/SendConfirmEmail",
                        data: { email: emailAdress },
                   
                        success: function(response){
                            if (response.isValid) {
                                swal("Отлично!",
                                    `На вашу почту (${emailAdress
                                    }) было отправлено письмо с ссылкой на активацию аккаунта. `,
                                    "info");
                            } else {
                                swal("Ошибка!",
                                    response.textMessage,
                                    "error");
                            }
                        }
                    });
                return false;
                }
                catch(ex)
                {
                    console.log(ex);
                }
            });
}

function callResetPassword() {

    swal({
            title: "Подтверждение почты",
            text: "Для отправки ссылки с активацией укажите вашу почту",
            type: "input",
            showCancelButton: true,
            closeOnConfirm: false,
            animation: "slide-from-top",
            inputPlaceholder: "Укажите почту"
        },
        function (emailAdress) {
            try {
                $.ajax({
                    type: 'POST',
                    url: "/Home/SendResetPassword",
                    data: { email: emailAdress },

                    success: function (response) {
                        if (response.isValid) {
                            swal("Отлично!",
                                `На вашу почту (${emailAdress
                                }) было отправлено письмо с ссылкой на сброс пароля. `,
                                "info");
                          
                        } else {
                            swal("Ошибка!",
                                response.textMessage,
                                "error");
                        }
                        
                    }
                });
                return false;
            }
            catch (ex) {
                console.log(ex);
            }
        });
}

jQueryAjaxPost = function (form, title, text, type) {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (response) {
           
                if (response.isValid) {
                        
                    $.get(null,
                        function (data) {
                            $('body').html(data);
                            sweetAlert(title, text, type,
                                {
                                    button: 'Хорошо'
                                });
                        });
                    $("#exampleModal .modal-body").html('');
                    $("#exampleModal .modal-title").html('');
                    $("#exampleModal").modal('hide');
           
                       
               
                }
             
                if (!response.isValid) {
                    $("#exampleModal .modal-body").html(response.html);
                    sweetAlert('Ошибка авторизации',
                        response.textMessage,
                        'error',
                        {
                            button: "Попробовать ещё раз",
                        });
                }
            },
            error: function(err) {
                console.log(err);
            }
        });
        return false;
    } catch (ex) {
        throw ex;
    }
}

signOut = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function(result) {
                $.get(null,
                    function(data) {
                        $('body').html(data);
                    });
            },
            error: function(error) {
                console.log(error);
            }
        });
        return false;
    } catch (ex) {
        throw ex;

    }
}

function changeResult() {
    $.get("/Fanfic/FanficList", function (data) {
        $('#likes').html(data);
        $('.namefanfic').html(data);
        $('.namefandom').html(data);
        $('.namecategory').html(data);
        $('.desciption').html(data);
        $('.tags').html(data);
    });
}
function changeImage() {

    $.get("/Home/MyInformation",
        function(data) {
            $("img.user-avatar").html(data);
        });
}

addLike = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                $.get(null,
                    function(data) {
                        $('body').html(data);
                    });
                changeResult();
            },
            error: function (err) {
                console.log(err);
            }
        });
        return false;
    } catch (ex) {
        console.log(ex);
    }
}

deleteFanfic = function(form, title, text, type ){
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (response) {
                $.get(null,
                    function (data) {
                        $('body').html(data);
                        sweetAlert(title, text, type,
                            {
                                button: 'Хорошо'
                            });
                    });
                
                $("#exampleModal .modal-body").html('');
                $("#exampleModal .modal-title").html('');
                $("#exampleModal").modal('hide');
            },
            error: function (err) {
                console.log(err);
            }
        });
        return false;
    } catch (ex) {
        throw ex;
    }
}

liveSearch = form => {

    $(document).ready(function () {
        $(".form-control").on("keyup",
            function () {
                var dataSearch = $(this).val().toLowerCase();
                $("#table1 .browser").each(function () {
                    var stringData = $(this).text().toLowerCase();
                    if (stringData.indexOf(dataSearch) === -1) {
                        $(this).hide();
                    } else {
                        $(this).show();
                    }
                });
            });
    });

}
