% Modified-Benjamin Eggs Test Matlab IO program
% Purpoes: Read dynamic dat aimported from Channel Simulator PCB via USB2.0
% interface

if ~(exist('figure(1)') | exist('figure(2)')) 
dlen = 251 ; %length(aan);
win = kaiser(dlen,9)';
win = win/sum(win);
figure(1)
subplot(2,2,1)
axis([-7.5/2 7.5/2 -20 40]);
grid on;
hold on;

subplot(2,2,2)
axis([-7.5/2 7.5/2 -20 40]);
grid on;
hold on;

subplot(2,2,3)
axis([-7.5/2 7.5/2 -20 40]);
grid on;
hold on;

subplot(2,2,4)
axis([-7.5/2 7.5/2 -20 40]);
grid on;
hold on;


figure(2)
subplot(2,2,1)
axis([1 dlen -130 130]);
grid on;
hold on;
subplot(2,2,2)
axis([1 dlen -130 130]);
grid on;
hold on;
subplot(2,2,3)
axis([1 dlen -130 130]);
grid on;
hold on;
xlabel('Sample')
ylabel('Amplitude')

subplot(2,2,4)
axis([1 dlen -130 130]);
grid on;
hold on;
set(figure(1),'DoubleBuffer','on')
set(figure(2),'DoubleBuffer','on')
end;


if fig_index == 0
    
   fid = fopen('bowstaff0.cdf', 'r');
   A = fread(fid, 512, 'uint8')';
   A = A - 128;
   aan = A(9:2:510-1)+ 1i*A(10:2:510);
  
   figure(1)
   subplot(2,2,1)
   plot((-.5:1/dlen:.5-1/dlen).*7.5, 20*log10(fftshift(abs(fft(aan.*win)))));
   fclose(fid);
   hold off;
   
   figure(2)
   subplot(2,2,1)
   plot(1:length(aan), real(aan),'b')
   hold on
   plot(1:length(aan), imag(aan), 'r');
 
   hold off;
   
elseif fig_index == 1
   
   fid = fopen('bowstaff1.cdf', 'r');
   A = fread(fid, 512, 'uint8')';
   A = A - 128;
   aan = A(9:2:510)+ 1i*A(10:2:510);
 
   figure(1)
   subplot(2,2,2)
   plot((-.5:1/dlen:.5-1/dlen).*7.5, 20*log10(fftshift(abs(fft(aan.*win)))));
   fclose(fid);
 hold off;
 
   figure(2)
   subplot(2,2,2)
   plot(1:length(aan), real(aan),'b')
   hold on
   plot(1:length(aan), imag(aan), 'r');
  
   hold off;
   
elseif fig_index == 2
   
   fid = fopen('bowstaff2.cdf', 'r');
   A = fread(fid, 512, 'uint8')';
   A = A - 128;
   aan = A(9:2:510)+ 1i*A(10:2:510);
   
   figure(1)
   subplot(2,2,3)
   plot((-.5:1/dlen:.5-1/dlen).*7.5, 20*log10(fftshift(abs(fft(aan.*win)))));
 
   fclose(fid);
 hold off;
   figure(2)
   subplot(2,2,3)
   plot(1:length(aan), real(aan),'b')
   hold on;
   plot(1:length(aan), imag(aan), 'r');
    hold off;
    
else
    
   fid = fopen('bowstaff3.cdf', 'r');
   A = fread(fid, 512, 'uint8')';
   A = A - 128;
   aan = A(9:2:510)+ 1i*A(10:2:510);

   figure(1)
   subplot(2,2,4)
   plot((-.5:1/dlen:.5-1/dlen).*7.5, 20*log10(fftshift(abs(fft(aan.*win)))));
  
   fclose(fid);
 hold off;
    figure(2)
   subplot(2,2,4)
   plot(1:length(aan), real(aan),'b')
   hold on
   plot(1:length(aan), imag(aan), 'r');
   hold off;
  
end;
hold off;

