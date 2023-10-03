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
                    <tr v-for="eventParticipant in eventParticipants" :key="eventParticipant.id">
                        <td>{{ getParticipantName(eventParticipant) }}</td>
                        <td>{{ eventParticipant.idCode }}</td>
                        <td><button @click="updateParticipant(eventParticipant.id)" class="btn btn-secondary btn-sm">Muuda</button>
                        <button @click="deleteParticipant(eventParticipant.id)" class="btn btn-danger btn-sm" style="margin-left:3px;">Kustuta osaleja</button></td>
                    </tr>
                </tbody>
            </table>
        </div>
</template>

<script setup lang="ts">
    import { Ref, ref, onMounted, defineProps, watch } from 'vue';
    import { useRoute, useRouter } from 'vue-router';
    import type { EventParticipant } from '../model/EventParticipant';

    const route = useRoute();
    const router = useRouter();
    const props = defineProps({participantsUpdated: Boolean});

    const eventParticipants: Ref<EventParticipant[]> = ref([]);

    const fetchParticipants = async () => {
        const eventId = route.params.eventId;
        console.log(eventId);
        try {
            const response = await fetch('https://localhost:7165/eventParticipant/list/' + eventId);

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            eventParticipants.value = data;
            console.log(eventParticipants.value);
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


    const updateParticipant = (id: String) => {
        router.push('/updateParticipant/' + id);
    };

    const deleteParticipant = async (id: String) => {
        try {
            const response = await fetch('https://localhost:7165/eventParticipant/' + id, {
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
