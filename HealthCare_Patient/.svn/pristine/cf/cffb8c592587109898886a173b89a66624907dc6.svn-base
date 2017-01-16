param($installPath, $toolsPath, $package, $project)

$project.Object.References | Where-Object { $_.Name -eq 'MobileEssentials.FormsIntellisense.Design' } | ForEach-Object { $_.Remove() }
$project.Object.References | Where-Object { $_.Name -eq 'Xamarin.Forms.Core.Design' } | ForEach-Object { $_.Remove() }