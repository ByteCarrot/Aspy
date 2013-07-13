function sleep(milliseconds) {
    var start = new Date().getTime();
    for (var i = 0; i < 1e7; i++) {
        if ((new Date().getTime() - start) > milliseconds) {
            break;
        }
    }
}

/* Methods for objects tree */
var sequence = 0;

function getIdentifier() {
    sequence++;
    return sequence;
}

function hasChildren(children) {
    return children != 'undefined' && children.length > 0;
}

function remove(id) {
    var rows = $('tr[data-parent=' + id + ']');
    rows.each(function () {
        remove($(this).attr('data-id'));
    });
    rows.remove();
}

function clicked () {
    var dom = $(this).get(0);
    if (!hasChildren(dom.x)) {
        return;
    }

    if (dom.expanded) {
        remove(dom.id);
        dom.expanded = false;

        var span = $(this).find('span.node');
        span.removeClass('minus');
        span.addClass('plus');
        return;
    }

    for (var i in dom.x) {
        render(dom.x[i], $(this), dom.id, dom.level);
    }

    dom.expanded = true;
    var span = $(this).find('span.node');
    span.removeClass('plus');
    span.addClass('minus');

    addPreview();

    refreshColResizable();
}

function renderEndOfTree(node, antecedent, parentId, level) {
    level++;
    antecedent.after($('#template-endoftree').render([{
        id: getIdentifier(),
        parentId: parentId,
        indent: level * 20 + 20
    }]));
}

function renderLeaf(node, antecedent, parentId, level) {
    level++;
    var html = $("#template-leaf").render([{
        id: getIdentifier(),
        indent: level * 20 + 20,
        parentId: parentId,
        name: node.name,
        value: node.value,
        type: node.type
    }]);

    antecedent.after(html);
}

function renderNode(node, antecedent, parentId, level) {
    level++;
    var id = getIdentifier();
    
    var html = $("#template-node").render([{
        id: id,
        indent: level * 20 + 20,
        bgpos: level * 20 + 2,
        cls: hasChildren(node.children) ? ' plus' : '',
        parentId: parentId,
        name: node.name,
        value: node.value,
        type: node.type
    }]);

    var row = $(html).click(clicked);
    antecedent.after(row);

    var dom = row.get(0);
    dom.x = node.children;
    dom.level = level;
    dom.id = id;
    dom.expanded = false;
}

function render(node, antecedent, parentId, level) {
    if (node.kind == 'end') {
        renderEndOfTree(node, antecedent, parentId, level);
    } else if (!hasChildren(node.children)) {
        renderLeaf(node, antecedent, parentId, level);
    } else {
        renderNode(node, antecedent, parentId, level);
    }
}

/* Functions for AJAX */

function ajaxStart() {
    $('#modal').css('display','block');
    $('#progress').css('display', 'block');
}

function ajaxComplete() {
    $('#modal').css('display', 'none');
    $('#progress').css('display', 'none');
    addPreview();
}

function load(index) {
    var what = index == 0 ? 'session' : 'cache';

    $.get('Aspy.ashx?action=' + what, function(nodes, status, xmlHttp) {
        if (status != 'success') {
            return;
        }
        var tbody = $('#tab-' + what + ' tbody');
        tbody.empty();
        tbody.append('<tr id="temporary-row" />');

        for (var i in nodes) {
            render(nodes[i], tbody.find('tr').last(), getIdentifier(), -1);
        }

        tbody.find('tr#temporary-row').remove();
    });

    refreshColResizable();
}

function addPreview() {
    $('td.value > div').each(function (index, value) {
        var cell = $(value);
        if (!cell.hasClass('preview')) {
            cell.addClass('preview');
            cell.append('<div class="preview-button"><img src="Aspy.ashx?preview.png"></img></div>');
            cell.hover(function () {
                $(this).find('div.preview-button').show();
            }, function () {
                $(this).find('div.preview-button').hide();
            });

            var div = cell.find('div.preview-button');
            div.hide();
            div.click(function (event) {
                event.stopPropagation();
                var text = $(this).siblings('span').html();

                $('div#preview-dialog').html(text)
                    .dialog({
                        modal: true,
                        height: 400,
                        width: 600,
                        closeOnEscape: true,
                        draggable: false,
                        resizable: false
                    });
            });
        }
    });
}

function refreshColResizable() {
    var index = $('#tabs').tabs('option', 'selected');
    var what = index == 0 ? 'session' : 'cache';
    var table = $('#tab-' + what + ' table');
    table.colResizable({ disable: true });
    table.colResizable();
}

$(document).ready(function () {
    ajaxComplete();
    $(document).ajaxStart(ajaxStart);
    $(document).ajaxComplete(ajaxComplete);

    $('#tabs').tabs({
        select: function (event, ui) {
            load(ui.index);
            refreshColResizable();
        },
        show: function (event, ui) {
            refreshColResizable();
        }
    });

    load(0);

    $('#refresh-button').click(function () {
        load($('#tabs').tabs('option', 'selected'));
    });
});