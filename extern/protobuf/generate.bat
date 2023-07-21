SET SRC_DIR="src"
SET DST_DIR="../../common/src/dist"

bin\protoc.exe  --csharp_out=%DST_DIR% %SRC_DIR%/addressbook.proto