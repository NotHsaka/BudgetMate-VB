BudgetMate (Visual Basic, Windows Forms, .NET 8)
A desktop version of the web BudgetMate dashboard.

HOW TO OPEN
1. Install Visual Studio 2022 with the ".NET desktop development" workload.
2. Double-click BudgetMate.sln, then press F5.

WHERE DATA IS SAVED
%AppData%\BudgetMate\budgetmate-data.json (paste that into File Explorer's address bar).
The JSON uses the same field names as the web version, so "Export JSON backup" files
can be imported in either app (Data > Import JSON / CSV).

FILES
BudgetMate\MainForm.vb           window, layout, and all actions
BudgetMate\MainForm.Painters.vb  drawing of the cards, charts and tables
BudgetMate\Dialogs.vb            Add/Edit transaction, goal, account, transfer, limits pop-ups
BudgetMate\Controls.vb           custom rounded controls
BudgetMate\Theme.vb              dark/light colors and fonts
BudgetMate\Models.vb             data classes and the category/payment lists
BudgetMate\DataStore.vb          saving and loading (the only file that touches disk)
BudgetMate\Program.vb            starts the app

NOT INCLUDED (web only): Supabase login / cloud sync, and Print / Save PDF.
