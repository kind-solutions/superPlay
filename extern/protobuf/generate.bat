SET SRC_DIR="include/src"
SET DST_DIR="../../common/src/dist"

bin\protoc.exe -I=%SRC_DIR% --csharp_out=%DST_DIR% %SRC_DIR%/*.proto