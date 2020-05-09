class CategoryManager {

    constructor() {

        this.addSelectCategoryElement = $('#add-select-category');

        this.selectCategoryElement = $(this.addSelectCategoryElement).find('#select-category');
        this.addCategoryElement = $(this.addSelectCategoryElement).find('#add-category');

        this.selectInputElement = $(this.selectCategoryElement).find('select');
        this.categoryNameInputElement = $(this.addCategoryElement).find('#category-name-input');
        this.errorSpanElement = $(this.addCategoryElement).find('#error-span-category');
    }

    onShowAddCategoryClick() {

        this.selectCategoryElement.hide();
        this.addCategoryElement.show();
    }

    onHideAddCategoryClick() {

        this.selectCategoryElement.show();
        this.addCategoryElement.hide();

        $(this.addCategoryElement).find('input').val('');
    }

    onClickAddCategory() {

        let data = this.getCategoryData();

        if (data != null)
            this.postCategory(data);
    }

    getRequestToken() {

        return $('[name=__RequestVerificationToken]').val();
    }

    getCategoryData() {

        let categoryName = this.categoryNameInputElement.val().trim();

        if (categoryName.length <= 0) {

            this.errorSpanElement.show();
            this.errorSpanElement.html('O nome da Categoria e obrigatório');
        }
        else if (categoryName.length > 40) {

            this.errorSpanElement.show();
            this.errorSpanElement.html('O nome da Categoria deve ter menos que 40 caracteres.');
        }
        else {

            this.errorSpanElement.hide();

            return {
                "name": categoryName
            }
        }

        return null;
    }

    postCategory(data) {

        let headers = {};
        headers['RequestVerificationToken'] = this.getRequestToken();

        $.ajax({
            url: '/admin/addcategory',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            headers: headers
        }).done(function (response) {

            if (response.isSuccess == false) {

                categoryManager.errorSpanElement.show();
                categoryManager.errorSpanElement.html(response.message);
            }
            else {

                let category = response.category;

                categoryManager.onHideAddCategoryClick();

                $(categoryManager.selectInputElement).append('<option value="' + category.id + '">' + category.name + '</option>');
                $(categoryManager.selectInputElement).selectpicker("refresh");
            }
        });
    }
}

var categoryManager = new CategoryManager();