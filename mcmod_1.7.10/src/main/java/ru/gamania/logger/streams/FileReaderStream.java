package ru.gamania.logger.streams;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class FileReaderStream {
    private final File log;

    public FileReaderStream(File file) {
        this.log = Paths.get(file.getParentFile().getAbsolutePath(), "logs", "latest.log").toFile();
    }

    public final List<String> getLines() {
        List<String> lines = new ArrayList<>();

        try (FileReader fileReader = new FileReader(this.log)) {
            Scanner scanner = new Scanner(fileReader);

            while (scanner.hasNextLine())
                lines.add(scanner.nextLine());
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }

        return lines;
    }
}
