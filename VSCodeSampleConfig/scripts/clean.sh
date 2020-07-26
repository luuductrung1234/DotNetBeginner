#!/bin/bash

declare $target=$1

dotnet clean $target

find . -iname "bin" -print0 | xargs -0 rm -rf
find . -iname "obj" -print0 | xargs -0 rm -rf