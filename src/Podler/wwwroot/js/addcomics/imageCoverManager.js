class ImageCoverManager {

    fileValidation(element) {
        let fileInput = element;
        let coverUploadDiv = $(element).parents('#cover-upload');
        let fileInputLabel = $(coverUploadDiv).find('label');
        let cover = $(coverUploadDiv).find('img');
        let errorSpan = $(coverUploadDiv).find('#error-span-cover');

        let allowedExtension = 'image/png';

        let file = fileInput.files[0];

        if (fileInput.files.length != 1) {

            this.showError(fileInput, 'Error, mais de uma imagem selecionada.');
        }
        else if (file.type != allowedExtension) {

            this.showError(fileInput, 'Somente imagens .png são aceitas.');
        }
        else if (file.size > 5242880) {

            this.showError(fileInput, 'Somente imagens menores que 5 MB.');
        }
        else {

            let reader = new FileReader();

            reader.onload = function (e) {
                $(cover).attr('src', e.target.result);
            }

            reader.readAsDataURL(file);
            errorSpan.hide();
            fileInputLabel.html(file.name);
        }
    }

    showError(fileInput, errorMessage) {

        let coverUploadDiv = $(fileInput).parents('#cover-upload');
        let fileInputLabel = $(coverUploadDiv).find('label');
        let cover = $(coverUploadDiv).find('img');
        let errorSpan = $(coverUploadDiv).find('#error-span-cover');

        errorSpan.show();
        fileInput.type = '';
        fileInputLabel.html('Selecione uma Imagem');
        fileInput.type = 'file';
        errorSpan.html(errorMessage);
        cover.attr("src", "/images/capa-vazia.png");
    }
}


var imageCoverManager = new ImageCoverManager();