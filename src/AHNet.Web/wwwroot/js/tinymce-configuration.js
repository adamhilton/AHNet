tinymce.init({
    selector: '#Body',
    width: 700,
    height: 300,
    plugins: [
        'codesample',
        'link'
    ],
    menubar: 'insert',
    content_css: '/css/content.css',
    toolbar: 'insertfile undo redo codesample | link | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | print preview media fullpage | forecolor backcolor emoticons '
});
