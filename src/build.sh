echo "\033[32mbuild managed plugins\033[m"
dotnet.exe build -c release -o ../lib/netstandard2.0

rm ../lib/netstandard2.0/Stride*.dll
rm ../lib/netstandard2.0/VL.Core*.dll