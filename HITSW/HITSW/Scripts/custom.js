$(document).ready(function() {
    //For main drop down
    $('.dropdown-toggle').dropdown();

    $("#addrbk_organization_details_menu li").click(function () {
        $("#addrbk_organization_details_menu li").removeClass('active');
        $(this).addClass('active');
    });

    $(".pagedList a").each(function () {
        $(this).click(function () {
            $a = $(this);
            var options = {
                url: $a.attr("href"),
                data: $("form").serialize(),
                type: "GET"
            };

            $.ajax(options).done(function (response) {
                var $target = $a.parents("div.pagedList").attr("data-hitsoft-target");
                $target.html(response);
            });
        });
    });

});

function fillMenu(url, target) {
    var options = {
        url: url,
        type: "GET"
    };

    $.ajax(options).done(function (data) {
        var $target = $(target);
        $target.html(data);
    });

    return false;
}


function ajaxFormSubmit() {
    var $form = $(this);

    $.ajax({
        url: $form.attr("action"),
        type: $form.attr("method"),
        data: $form.serialize(),
        success: function (response) {
            var $target = $($form.attr("data-hitsoft-target"));
            $target.html(response);
        }
    });

    return false;
}

function confirmDelete(btn) {
    if (confirm("Are you sure")) {
        var $a = $(btn);

        $.ajax({
            url: $a.attr("data-hitsoft-url"),
            type: "GET",
            success: function (response) {
                var $target = $($a.attr("data-hitsoft-target"));
                $target.html(response);
            }
        });
    }

    return false;
}

function getPagingList() {
    $a = $(this);

    $.ajax({
        url: $a.attr("href"),
        data: $("form").serialize(),
        type: "GET",
        success: function (response) {
            var $target = $($a.parents("div.pagedList").attr("data-hitsoft-target"));
            $target.html(response);
        }
    });

    return false;
}