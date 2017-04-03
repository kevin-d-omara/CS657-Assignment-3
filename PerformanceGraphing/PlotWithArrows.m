function PlotWithArrows(filename, specQuiver, specLine, specStart)
% Plot the points in filename showing arrows between consecutive points.
%   filename - name of the file with x-y values
%   specQuiver - linespec for quiver (arrows)
%   specLine   - linespec for full line
%   specStart  - linespec for starting point

data = importdata(filename);
points = data(1:end-1,:);
arrows = zeros(size(points,1), size(points,2));
for i=1:size(data,1)-1
    arrows(i,1) = data(i+1,1) - data(i,1);
    arrows(i,2) = data(i+1,2) - data(i,2);
end

hold on;
quiver(points(:,1), points(:,2), arrows(:,1), arrows(:,2), specQuiver, 'LineWidth', 2);
plot(data(:,1), data(:,2), specLine);
plot(data(1,1), data(1,2), specStart, 'LineWidth', 3);
xlim([0 30])
ylim([0 30])
grid