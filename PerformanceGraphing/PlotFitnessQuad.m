plotter = MakeAutoSubplot(2,2);
figure;

plotter()
hold on;
avg = importdata('ResultsAvg1.txt');
plot(abs(avg), 'b');
max = importdata('ResultsMax1.txt');
plot(abs(max), 'r');
title({'Fitness vs Generation', 'crossover = 70%', 'mutation = 10%', ...
    'chromosomes = 12'});
legend('show');
legend ('average fitness', 'best fitness');

plotter()
hold on;
avg = importdata('ResultsAvg2.txt');
plot(abs(avg), 'b');
max = importdata('ResultsMax2.txt');
plot(abs(max), 'r');
title({'Fitness vs Generation', 'crossover = 70%', 'mutation = 10%', ...
    'chromosomes = 12'});
legend('show');
legend ('average fitness', 'best fitness');


plotter()
hold on;
avg = importdata('ResultsAvg3.txt');
plot(abs(avg), 'b');
max = importdata('ResultsMax3.txt');
plot(abs(max), 'r');
title({'Fitness vs Generation', 'crossover = 70%', 'mutation = 10%', ...
    'chromosomes = 12'});
legend('show');
legend ('average fitness', 'best fitness');


plotter()
hold on;
avg = importdata('ResultsAvg4.txt');
plot(abs(avg), 'b');
max = importdata('ResultsMax4.txt');
plot(abs(max), 'r');
title({'Fitness vs Generation', 'crossover = 70%', 'mutation = 10%', ...
    'chromosomes = 12'});
legend('show');
legend ('average fitness', 'best fitness');