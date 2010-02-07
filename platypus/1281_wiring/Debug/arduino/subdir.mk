################################################################################
# Automatically-generated file. Do not edit!
################################################################################

# Add inputs and outputs from these tool invocations to the build variables 
C_SRCS += \
../arduino/WInterrupts.c \
../arduino/main.c \
../arduino/pins_arduino.c \
../arduino/wiring.c \
../arduino/wiring_analog.c \
../arduino/wiring_digital.c \
../arduino/wiring_pulse.c \
../arduino/wiring_shift.c 

OBJS += \
./arduino/WInterrupts.o \
./arduino/main.o \
./arduino/pins_arduino.o \
./arduino/wiring.o \
./arduino/wiring_analog.o \
./arduino/wiring_digital.o \
./arduino/wiring_pulse.o \
./arduino/wiring_shift.o 

C_DEPS += \
./arduino/WInterrupts.d \
./arduino/main.d \
./arduino/pins_arduino.d \
./arduino/wiring.d \
./arduino/wiring_analog.d \
./arduino/wiring_digital.d \
./arduino/wiring_pulse.d \
./arduino/wiring_shift.d 


# Each subdirectory must supply rules for building sources it contributes
arduino/%.o: ../arduino/%.c
	@echo 'Building file: $<'
	@echo 'Invoking: AVR Compiler'
	avr-gcc -Wall -g2 -gstabs -O2 -fpack-struct -fshort-enums -funsigned-char -funsigned-bitfields -DMAX_TIME_COUNT=F_CPU>>4-DNUM_LED_FLASHES=0 -DBAUD_RATE=57600 -mmcu=atmega1281 -DF_CPU=16000000UL -MMD -MP -MF"$(@:%.o=%.d)" -MT"$(@:%.o=%.d)" -c -o"$@" "$<"
	@echo 'Finished building: $<'
	@echo ' '


