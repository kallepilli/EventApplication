<template>
    <div class="col-md-6">
        <h3 class="mb-4">Muuda osaleja andmeid</h3>
        <form @submit.prevent="createParticipant">
            <div class="mb-3" v-if="!participant.IsCompany">
                <label class="form-label" for="firstName">Eesnimi</label>
                <input v-model="participant.FirstName" type="text" class="form-control" id="firstName" required />
            </div>
            <div class="mb-3" v-if="!participant.IsCompany">
                <label class="form-label" for="lastName">Perenimi</label>
                <input v-model="participant.LastName" type="text" class="form-control" id="lastName" required />
            </div>
            <div class="mb-3" v-if="participant.IsCompany">
                <label class="form-label" for="companyName">Ettev&otilde;tte nimi</label>
                <input v-model="participant.CompanyName" type="text" class="form-control" id="companyName" required />
            </div>
            <div class="mb-3">
                <label class="form-label" :for="idCodeLabel">{{ idCodeLabelText }}</label>
                <input v-model="participant.IdCode" type="text" class="form-control" id="idCode" required />
            </div>
            <div class="mb-3" v-if="participant.IsCompany">
                <label class="form-label" for="participantCount">Osalejate arv</label>
                <input v-model="participant.ParticipantCount" type="number" min="1" class="form-control" id="participantCount" required />
            </div>
            <div class="mb-3">
                <label class="form-label" for="paymentMethod">Maksemeetod</label><br />
                <select class="form-select form-select-lg" v-model="participant.PaymentMethod" id="paymentMethod" required>
                    <option :value="1">Panga &uuml;lekanne</option>
                    <option :value="2">Sularaha</option>
                </select>
            </div>
            <div class="mb-3">
                <label class="form-label" for="additionalInfo">Lisainfo ({{ charactersLeft }} / {{ additionalInfoMaxlength }})</label>
                <textarea v-model="participant.AdditionalInfo"
                          :maxlength="additionalInfoMaxlength"
                          class="form-control"
                          id="additionalInfo"
                          :class="{ 'is-invalid': characterLimitExceeded }"></textarea>
                <div v-if="characterLimitExceeded" class="text-danger">
                    Lisainfo &uuml;letab lubatud arvu {{ charactersLeft * -1 }} v&otilde;rra!
                </div>
            </div>
            <button type="submit" @click="submit()" :disabled="characterLimitExceeded || !isFilled()" class="btn btn-primary">Muuda osalejat</button>
        </form>
    </div>
</template>


<script setup lang="ts">
    import { useRoute, useRouter } from 'vue-router';
    import { Ref, ref, onMounted, computed } from 'vue';
    import type EventParticipantWithParticipant from '../model/EventParticipantWithParticipant';
    import type ParticipantDTO from '../model/ParticipantDTO';
    import type EventParticipantDTO from '../model/EventParticipantDTO';

    const route = useRoute();
    const router = useRouter();
    const eventParticipantId = route.params.eventParticipantId;
    const participant: Ref<EventParticipantWithParticipant> = ref({
        Id: eventParticipantId,
        EventId: '',
        IdCode: '',
        FirstName: '',
        LastName: '',
        CompanyName: '',
        ParticipantCount: 1,
        PaymentMethod: 0,
        AdditionalInfo: '',
        IsCompany: false
    });

    const fetchParticipant = async () => {
        const participantResponse: Ref<EventParticipantWithParticipant> = ref([]);
        try {
            const response = await fetch('https://localhost:7165/eventParticipant/participant/' + eventParticipantId);

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            participantResponse.value = data;
        } catch (error) {
            console.error('Error fetching participant', error);
        }
        if (!participantResponse.value.isCompany) {
            participant.value.FirstName = participantResponse.value.firstName;
            participant.value.LastName = participantResponse.value.lastName;
        }
        else {
            participant.value.CompanyName = participantResponse.value.companyName;
            participant.value.ParticipantCount = participantResponse.value.participantCount;
        }
        participant.value.ParticipantId = participantResponse.value.participantId;
        participant.value.EventId = participantResponse.value.eventId;
        participant.value.IdCode = participantResponse.value.idCode;
        participant.value.AdditionalInfo = participantResponse.value.additionalInfo;
        participant.value.PaymentMethod = participantResponse.value.paymentMethod;
        participant.value.IsCompany = participantResponse.value.isCompany;
    };

    onMounted(() => {
        fetchParticipant();
    });

    const submit = async () => {

        // Update participant
        const updateParticipant: Ref<ParticipantDTO> = ref({
            Id: participant.value.ParticipantId,
            IdCode: participant.value.IdCode,
            FirstName: participant.value.FirstName,
            LastName: participant.value.LastName,
            CompanyName: participant.value.CompanyName,
            IsCompany: participant.value.IsCompany
        });

        try {
            const response = await fetch('https://localhost:7165/participant/' + participant.value.ParticipantId, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(updateParticipant.value),
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
        } catch (error) {
            console.error('Error creating event:', error);
        }

        // Update eventParticipant
        const updateEventParticipant: Ref<EventParticipantDTO> = ref({
            EventId: participant.value.EventId,
            ParticipantId: participant.value.ParticipantId,
            ParticipantCount: participant.value.ParticipantCount,
            AdditionalInfo: participant.value.AdditionalInfo,
            PaymentMethod: participant.value.PaymentMethod
        });

        try {
            const response = await fetch('https://localhost:7165/eventParticipant/' + participant.value.Id, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(updateEventParticipant.value),
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
        } catch (error) {
            console.error('Error creating event:', error);
        }
        router.push('/eventDetails/' + participant.value.EventId);
    };

    const additionalInfoMaxlength = computed(() => (participant.value.IsCompany ? 5000 : 1500));
    const charactersLeft = computed(() => additionalInfoMaxlength.value - participant.value.AdditionalInfo.length);
    const characterLimitExceeded = computed(() => charactersLeft.value < 0);
    const idCodeLabelText = computed(() => (participant.value.IsCompany ? 'Reg. nr' : 'Isikukood'));
    const idCodeLabel = computed(() => (participant.value.IsCompany ? 'regNr' : 'idCode'));

    const isFilled = () => {
        if (!participant.value.IsCompany) {
            if (participant.value.FirstName == '' || participant.value.LastName == '' || participant.value.IdCode.length != 11 || participant.value.PaymentMethod == 0) {
                return false;
            }
        }
        else {
            if (participant.value.CompanyName == '' || participant.value.IdCode == '' || participant.value.PaymentMethod == 0) {
                return false;
            }
        }
        return true;
    };
</script>