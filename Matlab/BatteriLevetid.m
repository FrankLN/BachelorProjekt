function [voltage,tid]=BatteriLevetid
close all

%% Sæt værdier for afladning
t(1) = 1;
u= [3.2, 3.05, 2.96, 2.95, 2.94, 2.93, 2.91, 2.9, 2.88, 2.84, 2.78, 2.58, 2.48];
for n=2:1:13
    t(n)=(100/12)*(n-1);
end

tg(1) = 0;
for n=2:1:100*100
    tg(n) = tg(n-1) + 1;
end

tg = tg / 100;

ug = polyfit(t,u,10);
ut = polyval(ug, tg);
plot(t,u)
hold on

for i=1:2
    n=1;

    t_ny = t;%relativ tid
    tidsforskydelse=0;

    for n=1:50
        if n == 1;
            tid(1)=1;%rigtig tid
            voltage(1)=u(1);
        else
            %% find den næste tid
            tid(n)=tid(n-1)+(100/12)*rand(1);
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
        
        if voltage(n) < 2.5
            break
        end
    end
    result = []
    for n=1:1:numel(tid)
        number = ceil(tid(n)*100)/100
        index = find(tg==number)
        result(n) = voltage(n) - ut(index);
    end
    
    tid
    result
    plot(tid, result, 'green')
    
    if i == 1
        plot(tid,voltage, 'rx')
    end
    if i == 2
        plot(tid,voltage,'blackx')
    end
    if i == 3
        plot(tid,voltage,'yx')
    end
    if i == 4
        plot(tid,voltage,'cx')
    end
    if i == 5
        plot(tid,voltage,'gx')
    end
    t
    tid
end