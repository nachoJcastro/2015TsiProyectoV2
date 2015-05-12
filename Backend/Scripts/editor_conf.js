
var delay;
var editor = CodeMirror.fromTextArea(document.getElementById("code"), {
    lineNumbers: true,
    styleActiveLine: true,
    matchBrackets: true,
    extraKeys: { "Ctrl-Space": "autocomplete" },
    theme: "monokai"
});

/* editor.on("change", function() {
   clearTimeout(delay);
   delay = setTimeout(updatePreview, 300);
 });
 
 function updatePreview() {
   var previewFrame = document.getElementById('preview');
   var preview =  previewFrame.contentDocument ||  previewFrame.contentWindow.document;
   preview.open();
   preview.write(editor.getValue());
   preview.close();
 }
 setTimeout(updatePreview, 300);*/
