def first_fit_con_compactacion(memoria, procesos):
    while True:
        # Asignar procesos
        for proceso in procesos:
            espacio = encontrar_espacio_libre(memoria, proceso.tamanio)
            if espacio is not None:
                memoria[espacio:espacio + proceso.tamanio] = [proceso.nombre] * proceso.tamanio
                break
        else:
            # Compactar memoria
            memoria = compactar_memoria(memoria)
            if len(procesos) == 0:
                return memoria

def compactar_memoria(memoria):
    espacios_libres = []
    for i in range(len(memoria)):
        if memoria[i] == 0:
            espacios_libres.append(i)

    for i in range(len(espacios_libres)):
        for j in range(i + 1, len(espacios_libres)):
            if espacios_libres[i] + espacios_libres[j] >= 0:
                espacios_libres[i] += espacios_libres[j]
                espacios_libres.pop(j)

    for i in range(len(espacios_libres)):
        memoria[espacios_libres[i]] = [0] * abs(espacios_libres[i])

    return memoria
