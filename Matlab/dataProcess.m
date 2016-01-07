load('data.mat');

plot(A(1,:),A(2,:))
hold on
plot(Data(1,:),Data(2,:), 'green')

indices = zeros(1,6);
Diff = [];
for i = 1:6
    idx = find(B(1,:) >= Data(1,i))
    Diff(i) = Data(2,i)-B(2,idx(1));
    indices(i) = idx(1);
end
%plot(Data(1,:),Diff, 'r')
plot(Data(1,:),abs(Diff), 'r')
%plot(Data(1,:),-abs(Diff), 'r')

figure,
scatter(Data(2,1:6),B(2,indices))

[h,p,ci,stats] = ttest(Data(2,1:6),B(2,indices))