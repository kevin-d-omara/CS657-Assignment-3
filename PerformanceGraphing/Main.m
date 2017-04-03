load Results.txt;
[n, p] = size(Results);

points = Results(1:end-1,:);
arrows = zeros(size(points,1), size(points,2));
for i=1:size(Results,1)-1
    arrows(i,1) = Results(i+1,1) - Results(i,1);
    arrows(i,2) = Results(i+1,2) - Results(i,2);
end

hold on;
quiver(points(:,1), points(:,2), arrows(:,1), arrows(:,2), 'bo', 'LineWidth', 2);
plot(Results(:,1), Results(:,2), 'k--');
plot(Results(1,1), Results(1,2), 'ro', 'LineWidth', 3);
xlim([0 30])
ylim([0 30])
grid