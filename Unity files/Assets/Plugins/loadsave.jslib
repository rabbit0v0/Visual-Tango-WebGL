mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
  },

  HelloString: function (str) {
    window.alert(Pointer_stringify(str));
  },

  PrintFloatArray: function (array, size) {
    for(var i = 0; i < size; i++)
    console.log(HEAPF32[(array >> 2) + i]);
  },

  AddNumbers: function (x, y) {
    return x + y;
  },

  StringReturnValueFunction: function () {
    var returnStr = "bla";
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },

  BindWebGLTexture: function (texture) {
    GLctx.bindTexture(GLctx.TEXTURE_2D, GL.textures[texture]);
  },

  SaveTextAsFile: function (filename, text) {
    var element = document.createElement('a');
    var text_content = Pointer_stringify(text);
    var file_name_content = Pointer_stringify(filename);
    element.setAttribute('href',
     'data:text/plain;charset=utf-8,' + encodeURIComponent(text_content));
    element.setAttribute('download', file_name_content);
    element.style.display = 'none';
    document.body.appendChild(element);
    element.click();
    document.body.removeChild(element);
  },

  LoadFileToLocalStorage: function() {
    if (!document.getElementById('FileUploadingPluginInput'))
      Init();

    function put(key, value) {
      if (window.localStorage) {
        window.localStorage[key] = value;
      }
    }

    function Init() {
      var inputFile = document.createElement('input');
      inputFile.setAttribute('type', 'file');
      inputFile.setAttribute('id', 'FileUploadingPluginInput');

      inputFile.style.visibility = 'hidden';

      inputFile.onclick = function (event) {
          this.value=null;
      };

      inputFile.onchange = function (evt) {
          //process file
          evt.stopPropagation();
          var fileInput = evt.target.files;
          if (!fileInput || !fileInput.length) {
              put("LoadedContent", "");
              put("LoadDone", "true");
              return; // "no file selected"
          }
          var fileReader = new FileReader();
          fileReader.onload = function(fileLoadedEvent)
          {
              var content = fileLoadedEvent.target.result;
              put("LoadedContent", content);
              put("LoadDone", "true");
          };
          fileReader.readAsText(fileInput[0], "UTF-8");
      }
      document.body.appendChild(inputFile);
    }
    document.getElementById('FileUploadingPluginInput').click();
  },

  CheckLoadDone : function() {
    function get(key) {
      return window.localStorage ? window.localStorage[key] : null;
    }
    var done = get("LoadDone");
    if (done == "false") {
      return -1;
    }
    return 1;
  },

  GetLoadedString : function() {
    function get(key) {
      return window.localStorage ? window.localStorage[key] : null;
    }
    var content = get("LoadedContent");
    var bufferSize = lengthBytesUTF8(content) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(content, buffer, bufferSize);
    return buffer;
  },

  RefreshStorage : function() {
    function put(key, value) {
      if (window.localStorage) {
        window.localStorage[key] = value;
      }
    }
    put("LoadedContent", "");
    put("LoadDone", "false");
  },

});
