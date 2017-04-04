plotter = MakeAutoSubplot(1,1);
figure;

plotter()
hold on;
avg = importdata('ResultsAvg.txt');
plot(abs(avg), 'b');
max = importdata('ResultsMax.txt');
plot(abs(max), 'r');
title({'Fitness vs Generation', 'crossover = 70%', 'mutation = 50%', ...
    'chromosomes = 100'});
legend('show');
legend ('average fitness', 'best fitness');