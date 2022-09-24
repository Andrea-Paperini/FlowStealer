Applicazione console che trasmette un flusso video creato da VLC come HLS creando in uscita un formato .ogg con video e audio con i parametri vcodec=theo,vb=1600,scale=1,acodec=none
e bufferizzando per mantenere la fluidità più solida.
Il flusso sarà disponibile all'indirizzo ip:porta/stream.ogg
nota: il flusso è perfettamente compatibile con gli standard HTML5 (perciò è possibile visualizzare il contenuto dal proprio sito internet) con il seguente tag
⬇️⬇️⬇️⬇️⬇️
<video controls>
  <source src="ip:porta/stream.ogg" type="video/ogg">
</video>

è necessario il Framework Net 5
