package ru.gamania.logger;

import cpw.mods.fml.common.Mod;
import cpw.mods.fml.common.event.FMLPreInitializationEvent;
import ru.gamania.logger.sockets.ClientSocket;
import ru.gamania.logger.streams.FileReaderStream;

import java.io.IOException;
import java.net.InetAddress;
import java.util.Timer;
import java.util.TimerTask;

@Mod(modid = "ModLogger", name = "ModLogger")
public class ModLogger {
    private final Timer timer = new Timer();

    private ClientSocket clientSocket;
    private FileReaderStream fileReaderStream;

    @Mod.EventHandler
    public void preInit(FMLPreInitializationEvent event) throws IOException {
        this.clientSocket = new ClientSocket(InetAddress.getByName("127.0.0.1"), 8005);
        this.fileReaderStream = new FileReaderStream(event.getModConfigurationDirectory());

        this.timer.scheduleAtFixedRate(new TimerTask() {

            @Override
            public synchronized void run() {
                clientSocket.writeList(fileReaderStream.getLines());

                if (clientSocket.hasNewLines()) {
                    try {
                        clientSocket.writeSocket();
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
            }
        }, 0, 100L);
    }
}
