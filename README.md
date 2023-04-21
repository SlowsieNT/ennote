# ennote - Encrypted Note(s)

## This is free and unencumbered software released into the public domain.
![Visitor count](https://shields-io-visitor-counter.herokuapp.com/badge?page=slowsient.ennote)

## Info
App uses AES encryption.<br>
Binaries may be updated, so check binary date/version.<br>

## Requirements
Windows XP (*minimum*)<br>
.NET 3.5; for Windows XP: [Download Link](https://www.microsoft.com/en-us/download/details.aspx?id=25150)<br>
(RAM usage is around 8MB)<br>

## Why reinvent the 'wheel?'
The objective was to make it uncopyrighted; meaning I do not claim any rights to the source code.<br>

## Tips
You can rename note by `Ctrl+R` or by double clicking title of the note.<br>
You can save note as plain text by `Ctrl+Shift+S` and selecting "Text Documents" option.<br>
You can open plain text by `Ctrl+O`, and after decryption fails, it will ask whether you want it to load as plain text.<br>

## Screenshot(s)
<img alt="App screenshot" src="https://github.com/SlowsieNT/ennote/raw/main/etc/app.png"><br>
Right click on RTextbox<br>
<img alt="RTextbox context menu" src="https://github.com/SlowsieNT/ennote/raw/main/etc/rtCtx.png"><br>
Button "+" may be right clicked to see options such as:<br>
`Open...` and `Set current directory`<br>
<img alt="Plus button context menu screenshot" src="https://raw.githubusercontent.com/SlowsieNT/ennote/main/etc/plusCtx.png"><br>
Button "..." may be clicked, or right clicked to see options such as:<br>
`New Password`, `Delete Note`, `Register File Extension`, `Open Containing Folder`.<br>
<img alt="... button context menu screenshot" src="https://raw.githubusercontent.com/SlowsieNT/ennote/main/etc/tdotCtx.png"><br>

## Command Line syntax

`progName <FileName> <Password>` to open file with password.<br>
or<br>
`progName <FileName>` - to open file without password.<br>
or EVEN:<br>
`progName <FolderName>` - to set as working directory.<br>

## License
All of files are **Unlicense**d. No rights are reserved.
