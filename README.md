# JsonConfigForNetCoreConsoleApp

The updateable settings for the .Net Core console application like the "settings" in the .Net Framework. 

Just inherit the BaseJsonConfig class and create you own parametrs to work with the json setting file. 

See [example](https://github.com/dbobylev/JsonConfigForNetCoreConsoleApp/blob/master/JsonConfigForNetCoreConsoleApp.Example/Config.cs).

Friendly types:

| Param Type   | Getters             | Setters             |
|--------|------------------|------------------|
| string | GetValue\<string\>|  SetValue |
| int | GetValue\<int\>|  SetValue |
| int? | GetNullableValue\<int\>|  SetValue |
| bool | GetValue\<bool\>|  SetValue |
| bool? | GetNullableValue\<bool\>|  SetValue |
| float | GetValue\<float\>|  SetValue |
| float? | GetNullableValue\<float\>|  SetValue |
| string[] | GetEnumerableValue\<string\>|  SetValue |
| int[] | GetEnumerableValue\<int\>|  SetValue |
