################################################################################
# Automatically-generated file. Do not edit!
################################################################################

-include ../makefile.init

RM := rm -rf

# All of the sources participating in the build are defined here
-include sources.mk
-include subdir.mk
-include objects.mk

ifneq ($(MAKECMDGOALS),clean)
ifneq ($(strip $(ASM_DEPS)),)
-include $(ASM_DEPS)
endif
ifneq ($(strip $(S_UPPER_DEPS)),)
-include $(S_UPPER_DEPS)
endif
ifneq ($(strip $(S_DEPS)),)
-include $(S_DEPS)
endif
ifneq ($(strip $(C_DEPS)),)
-include $(C_DEPS)
endif
endif

-include ../makefile.defs

# Add inputs and outputs from these tool invocations to the build variables 
LSS += \
1281_boot.lss \

SIZEDUMMY += \
sizedummy \


# All Target
all: 1281_boot.elf secondary-outputs

# Tool invocations
1281_boot.elf: $(OBJS) $(USER_OBJS)
	@echo 'Building target: $@'
	@echo 'Invoking: AVR C Linker'
	avr-gcc -Wl,-Map,1281_boot.map -mmcu=atmega1281 -o"1281_boot.elf" $(OBJS) $(USER_OBJS) $(LIBS)
	@echo 'Finished building target: $@'
	@echo ' '

1281_boot.lss: 1281_boot.elf
	@echo 'Invoking: AVR Create Extended Listing'
	-avr-objdump -h -S 1281_boot.elf  >"1281_boot.lss"
	@echo 'Finished building: $@'
	@echo ' '

sizedummy: 1281_boot.elf
	@echo 'Invoking: Print Size'
	-avr-size --format=avr --mcu=atmega1281 1281_boot.elf
	@echo 'Finished building: $@'
	@echo ' '

# Other Targets
clean:
	-$(RM) $(ELFS)$(ASM_DEPS)$(OBJS)$(S_UPPER_DEPS)$(LSS)$(S_DEPS)$(C_DEPS)$(SIZEDUMMY) 1281_boot.elf
	-@echo ' '

secondary-outputs: $(LSS) $(SIZEDUMMY)

.PHONY: all clean dependents
.SECONDARY:

-include ../makefile.targets
