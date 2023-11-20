Codeback is a personal knowledge management tool for programmers, mainly solving the collection and query of offline knowledge. 
Chinese note reads as follows: <https://github.com/tpsimon1/codeback/README_ZH.md>
Main features:
1. Windows 10/11 use, dependent on .NET Framework 4.8 to run, no other system support plan.
2. The mode of storing a new file in the sqlite file and creating a new file every month solves the problem of capacity limit.
3. Dual-mode display of RichText and WebHTML to display text and HTML content.
4. The image paste in the .html web page is stored locally in binary base64 to avoid the problem that the content cannot be viewed due to the failure of the web page.
5. You can run the program asynchronously through keywords, including Cmd, CSScript (can dynamically run C#), Python.
Python comes with version 3.12, which can be replaced by other versions, but you must ensure that the paths of python.exe and pip.exe are correct
6. Files can be uploaded to the database, it is recommended to upload only part of the problem that is less than 100M, to avoid the large file size of a single sqlite database.
7. All functions are run locally and green, and there are no functions such as upload and data synchronization to ensure data security. If you need to synchronize data in multiple environments, you can use file synchronization tools to solve it, such as google driver, Baidu cloud disk, FreeFileSync, etc.
You can also use a USB flash drive as a portable disk tool.
8. For the time being, it is Chinese version, and multi-language support is still planned.
