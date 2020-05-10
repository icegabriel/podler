class SelectAddManager {

    constructor(route, addSelectId, selectDivId, addDivId, nameInputId, errorSpanId) {

        this.route = route;

        this.addSelectDivElement = $(addSelectId);

        this.selectDivElement = $(this.addSelectDivElement).find(selectDivId);
        this.addDivElement = $(this.addSelectDivElement).find(addDivId);

        this.selectInputElement = $(this.selectDivElement).find('select');
        this.nameInputElement = $(this.addDivElement).find(nameInputId);
        this.errorSpanElement = $(this.addDivElement).find(errorSpanId);
    }

    onShowAddClick() {

        this.selectDivElement.hide();
        this.addDivElement.show();
    }

    onHideAddClick() {

        this.selectDivElement.show();
        this.addDivElement.hide();

        $(this.addDivElement).find('input').val('');
    }

    onClickAdd() {

        let data = this.getData();

        if (data != null)
            this.postData(data);
    }

    getRequestToken() {

        return $('[name=__RequestVerificationToken]').val();
    }

    getData() {

        let name = this.nameInputElement.val().trim();

        if (name.length <= 0) {

            this.errorSpanElement.show();
            this.errorSpanElement.html('O nome e obrigatório');
        }
        else if (name.length > 40) {

            this.errorSpanElement.show();
            this.errorSpanElement.html('O nome deve ter menos que 40 caracteres.');
        }
        else {

            this.errorSpanElement.hide();

            return {
                "name": name
            }
        }

        return null;
    }

    postData(data) {

        let headers = {};
        headers['RequestVerificationToken'] = this.getRequestToken();

        $.ajax({
            url: this.route,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            headers: headers
        }).done((response) => this.onDonePost(response));
    }

    onDonePost(response) {

        if (response.isSuccess == false) {

            this.errorSpanElement.show();
            this.errorSpanElement.html(response.message);
        }
        else {

            let model = response.model;

            this.onHideAddClick();

            $(this.selectInputElement).append('<option value="' + model.id + '">' + model.name + '</option>');
            $(this.selectInputElement).selectpicker("refresh");
        }
    }
}

var categoryManager = new SelectAddManager('/admin/addcategory',
    '#add-select-category',
    '#select-div-category',
    '#add-div-category',
    '#category-name-input',
    '#error-span-category');

var authorManager = new SelectAddManager('/admin/addauthor',
    '#add-select-author',
    '#select-div-author',
    '#add-div-author',
    '#author-name-input',
    '#error-span-author');

var designerManager = new SelectAddManager('/admin/adddesigner',
    '#add-select-designer',
    '#select-div-designer',
    '#add-div-designer',
    '#designer-name-input',
    '#error-span-designer');