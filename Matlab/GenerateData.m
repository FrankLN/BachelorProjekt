filename = 'testdata.xlsx';

T = [rand(1)];

i = 1;
while(i < 2000)
    T = [T;rand(1)];
    i = i + 1;
end

xlswrite(filename,T,'Sheet1','B2');
