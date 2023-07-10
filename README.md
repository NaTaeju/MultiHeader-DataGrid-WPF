# MultiHeader
- When displaying a table using a data grid, it is often easier to understand the table if the header is displayed in multiple rows, but the data grid has one row of header. To solve this inconvenience, it is difficult to create a new data grid, so we created it to solve it in the form of an extension function.
- It was made with reference to open sources. In the open sources, if you can adjust the width of the column, there are inconveniences such as not being able to scroll, so I made it myself.
- I couldn't solve the case of moving the column by dragging it. I want to block Drag, but it seems there is no property.
- If scrolling does not occur due to the small number of rows in the DataGrid, try attaching the footer below the last row. However, if you do not set the DataGrid RowHeight, it is difficult to measure the size.
- If you find a solution, I would appreciate it if you could tell me how. ^^;
![image](https://github.com/NaTaeju/MultiHeader-DataGrid-WPF/assets/96507992/6af0e44b-8ef0-47ba-aeeb-9189f83b1712)
