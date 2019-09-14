# 6502sharp
MOS Technology 6502 CPU emulator library written in .NET Standard 2.0 and .NET Core 2.1. Both NMOS and CMOS variants inclued + all NMOS undocumented opcodes.
## Features

- Full MOS Technology 6502 emulator
  - Both NMOS and CMOS variants
- Unit tests for complex instructions
  - Also includes one test ROM that tests the actual runtime
- **2 Sample applications using the library**
  - [AsmConsole](/Examples/AsmConsole)
    - Runs simple games from http://6502asm.com/
  - [ehBasic](/Examples/ehBasic)
    - Runs the Enhance BASIC ROM
    - Manual: http://www.sunrise-ev.com/photos/6502/EhBASIC-manual.pdf
- Higly customizable
  - Listen to events (OnTick, OnInstruction...)
  - Modify or add new instructions
  - Create custom memory logic
- Continuous Integration
  - Includes build and tests
It tries to emulate the chip as closely as possible but it has some timing issues.

## Performance

**It's really slow**

It can run simpler programs at full speed but overall it's not really usable in things like a NES emulator. It's more of a research project.
