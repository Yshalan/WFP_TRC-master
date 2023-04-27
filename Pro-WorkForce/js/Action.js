

function ShowMessage(msg) {
    Lobibox.alert('info', {
        title: '&nbsp;',
        size: 'large',
        // delay: false,
        //img: '../images/CompaniesLogo/41.png',
        rounded: false,
        //  delay: false,
        draggable: false,
        closeOnEsc: false,
        //delay: 50000,
        // position: 'center top',
        position: {
            left: 500, top: 200
        },
        msg: msg,
        buttons: {
            ok: {
                'class': 'lobibox-btn lobibox-btn-default',
                //  onClickUrl: link,
                text: 'OK',
                closeOnClick: true

            }
        }


    });

}

function ShowMessage_Success(msg) {
    Lobibox.alert('success', {
        title: '&nbsp;',
        size: 'large',
        // delay: false,
        //img: '../images/CompaniesLogo/41.png',
        rounded: false,
        //  delay: false,
        draggable: false,
        closeOnEsc: false,
        //  delay: 5000,
        // position: 'center top',
        position: {
            left: 500, top: 200
        },
        msg: msg,
        buttons: {
            ok: {
                'class': 'lobibox-btn lobibox-btn-default',
                //  onClickUrl: link,
                text: 'OK',
                closeOnClick: true

            }
        }


    });

}

function ShowMessage_Error(msg) {
    Lobibox.alert('error', {
        title: '&nbsp;',
        size: 'large',
        // delay: false,
        //img: '../images/CompaniesLogo/41.png',
        rounded: false,
        //  delay: false,
        draggable: false,
        closeOnEsc: false,
        //  delay: 5000,
        // position: 'center top',
        position: {
            left: 500, top: 200
        },
        msg: msg,
        buttons: {
            ok: {
                'class': 'lobibox-btn lobibox-btn-default',
                //  onClickUrl: link,
                text: 'OK',
                closeOnClick: true

            }
        }


    });

}

function ShowMessage_Warning(msg) {
    Lobibox.alert('warning', {
        title: '&nbsp;',
        size: 'large',
        // delay: false,
        //img: '../images/CompaniesLogo/41.png',
        rounded: false,
        //  delay: false,
        draggable: false,
        closeOnEsc: false,
        //  delay: 5000,
        // position: 'center top',
        position: {
            left: 500, top: 200
        },
        msg: msg,
        buttons: {
            ok: {
                'class': 'lobibox-btn lobibox-btn-default',
                //  onClickUrl: link,
                text: 'OK',
                closeOnClick: true

            }
        }


    });

}

function ShowMessageAndRedirect(msg, link) {
    Lobibox.alert('success', {
        title: '&nbsp;',
        size: 'large',
        // delay: false,
        //img: '../images/CompaniesLogo/41.png',
        rounded: false,
        //  delay: false,
        draggable: false,
        closeOnEsc: false,
        //  delay: 5000,
        //  position: 'center top',
        onClickUrl: link,
        position: {
            left: 500, top: 200
        },
        msg: msg,
        buttons: {
            ok: {
                'class': 'lobibox-btn lobibox-btn-default',
                onClickUrl: link,
                text: 'OK'
                //  closeOnClick: true,

            }
        },
        beforeClose: function (lobibox) {
            window.location = link
        }

    });


}

function Confirm_Accept_Request(message) {
    Lobibox.confirm({
        msg: message,
        callback: function (result) {
            if (type == 'yes') {
                result = true;
                return true;
            }
            else {
                result = false
                return false;
            }

        }
    });
    return false;
}

function notify_Requests(title, message, lang) {
    var left;
    if (lang == 'ar') {
        var direction = 'bottom left'

    }
    else {
        var direction = 'bottom right'
    }
    Lobibox.notify('info', {
        size: 'mini',
        pauseDelayOnHover: true,
        delay: 5000,
        delayIndicator: true,
        continueDelayOnInactiveTab: false,
        showClass: 'fadeInDown',
        hideClass: 'fadeUpDown',
        closeOnClick: true,
        title: title,
        position: direction,
        msg: message
    });
}

function notify(title, message, lang) {
    var left;
    if (lang == 'ar') {
        var left = 150
    }
    else {
        var left = 600
    }
    Lobibox.notify('default', {

        rounded: true,
        pauseDelayOnHover: true,
        continueDelayOnInactiveTab: false,
        title: title,
        position: {
            left: left, top: 400
        },
        msg: message
    });
}

function OpenlobiWindow(title, url) {
    Lobibox.window({
        title: title,
        url: url,
        draggable: true,
        height: 530,
        width: 700,
        autoload: true,
        loadMethod: 'GET',
        showAfterLoad: false
    });

}

function ValidateDeletedGridWithConfirmation(msg, gridClientID, senderClientID, Lang) {
    var grid = $find(gridClientID);
    var masterTable = grid.get_masterTableView();
    var value = false;
    for (var i = 0; i < masterTable.get_dataItems().length; i++) {
        var gridItemElement = masterTable.get_dataItems()[i].findElement("chk");
        if (gridItemElement.checked) {
            value = true;
        }
    }
    if (value === false) {
        Lobibox.alert("warning",
        {
            msg: msg
        });
        return false;
    } else {
        if (Lang == 'AR') {
            DeleteConfirmation("هل انت متأكد من الحذف؟", senderClientID);
            return false;
        }
        else {
            DeleteConfirmation("Are you sure you want to delete?", senderClientID);
            return false;
        }
    }
    //return value;
}

function DeleteConfirmation(msg, UniqueID, lang) {
    var title = "Delete confirmation";
    var yes = "yes";
    var no = "no";
    if (lang == "ar-JO") {
        title = "تأكيد الحذف"
        yes = "نعم";
        no = "لا";
    }
    var rtn = false;
    Lobibox.confirm({
        msg: msg,
        title: title,
        buttons: {
            yes: {
                'class': 'lobibox-btn lobibox-btn-cancel',
                text: yes,
                closeOnClick: true
            },
            no: {
                'class': 'lobibox-btn btn-success',
                text: no,
                closeOnClick: true
            }
        },
        callback: function ($this, type, ev) {
            console.log(type);
            if (type == "yes") {
                console.log(UniqueID);
                __doPostBack(UniqueID, '')
            }
        }
    });
    return false;
}
