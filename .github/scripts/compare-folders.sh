#!/bin/sh

# Specify the folders to compare
FOLDER1="Assets/Demo"
FOLDER2="Assets/VFX-Text/Samples~/Demo"

# Compare folders using diff command
DIFF_OUTPUT=$(diff -rq $FOLDER1 $FOLDER2)

# Check if there are differences
if [ -z "$DIFF_OUTPUT" ]; then
  echo "Folders are the same."
else
  echo "Folders are not the same."
  echo "$DIFF_OUTPUT"
  
  # Exit with a non-zero status code to indicate failure
  exit 1
fi
