function [voltage,tid]=BatteriLevetid
close all

%% Antal afladninger der skal genereres
myGenNumber = 1000;

%% Sæt værdier for afladning
t(1) = 1;
u= [3.2, 3.05, 2.96, 2.95, 2.94, 2.93, 2.91, 2.9, 2.88, 2.84, 2.78, 2.58, 2.48];
for n=2:1:13
    t(n)=(100/12)*(n-1);
end

plot(t,u)
hold on

result = [];

for i=1:myGenNumber
    n=1;

    t_ny = t;%relativ tid
    tidsforskydelse=0;

    for n=1:13
        if n == 1;
            tid(1)=1;%rigtig tid
            voltage(1)=u(1);
        else
            %% find den næste tid
            %tid(n)=tid(n-1)+(100/12)*rand(1);
            tid(n)=(100/12)*(n-1);
            %% find forventningsværdi af voltage(n)
            bb=find(t_ny<tid(n));
            U1=u(bb(end));
            t1=t_ny(bb(end));

            if length(bb)==length(u)
                U2=NaN;
            else
                U2=u(bb(end)+1);
                t2=t_ny(bb(end)+1);
            end

            %% Find den Rayleigh fordeling, der passer.
            fU_ny=(U2-U1)/(t2-t1)*(tid(n)-t1)+U1;
            %find sigma
            if fU_ny<voltage(n-1)
                sigma=(voltage(n-1)-fU_ny)*sqrt(2/pi);
            else
                sigma=0;
            end

            %Beregn den nye spænding ved at trækket en rayleighfordelt værdi.
            voltage(n)=voltage(n-1)-sqrt(-log(1-rand(1))*2*sigma^2);

            %% ryk spændingskurven
            bb=find(voltage(n)<u);
            U1=u(bb(end));
            t1=t_ny(bb(end));

            if length(bb)==length(u)
                U2=NaN;
            else
                U2=u(bb(end)+1);
                t2=t_ny(bb(end)+1);
                A=(U2-U1)/(t2-t1);
                B=U2-A*t2;
                told=(voltage(n)-B)/A;
                tidsforskydelse=tid(n)-told;
                t_ny=t_ny+tidsforskydelse;
            end
        end
        plot(tid, voltage, 'x')
    end
    if numel(result) < 1
        result = [voltage]
    else
        result = [result ; voltage];
    end
end

r = result(1,:);
for i=2:myGenNumber
    r = r + result(i,:);
end
r = r / myGenNumber;

plot(tid, r, 'red')