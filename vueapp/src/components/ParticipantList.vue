<template>
        <div class="col-md-6">
            <h3>Registreerunud osalejad</h3>
            <table class="table table-hover">
                <thead>
                    <tr>
                        <th>Nimi</th>
                        <th>Isikukood/Reg. kood</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="participant in participants" :key="participant.id">
                        <td>{{ getParticipantName(participant) }}</td>
                        <td>{{ participant.idCode }}</td>
                        <td><button @click="changeParticipant(participant.id)" class="btn btn-secondary btn-sm">Muuda</button>
                        <button @click="deleteParticipant(participant.id)" class="btn btn-danger btn-sm" style="margin-left:3px;">Kustuta osaleja</button></td>
                    </tr>
                </tbody>
            </table>
        </div>
</template>

<script setup lang="ts">
    import { Ref, ref, onMounted, defineProps, watch } from 'vue';
    import { useRoute } from 'vue-router';
    import type { EventParticipant } from '../model/EventParticipant';

    const route = useRoute();
    const props = defineProps({
        participantsUpdated: Boolean,
    });

    const participants: Ref<EventParticipant[]> = ref([]);

    const fetchParticipants = async () => {
        const eventId = route.params.eventId;
        console.log(eventId);
        try {
            const response = await fetch('https://localhost:7165/participant/list/' + eventId);

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            participants.value = data;
        } catch (error) {
            console.error('Error fetching events:', error);
        }
    };

    const getParticipantName = (participant: any) => {
        console.log(participant.isCompany);
        if (participant.isCompany == true) {
            return participant.companyName;
        }
        return participant.firstName + ' ' + participant.lastName;

    };


    const changeParticipant = (id: String) => {
        router.push('/changeParticipant/' + id);
    };





    const deleteParticipant = async (id: String) => {
        try {
            const response = await fetch('https://localhost:7165/participant/' + id, {
                method: 'DELETE'
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            fetchParticipants();
        } catch (error) {
            console.error('Error fetching events:', error);
        }
    };


    watch(
        () => props.participantsUpdated,
        (value) => {
            fetchParticipants();
        }
    )


    onMounted(() => {
        fetchParticipants();
    });
</script>
